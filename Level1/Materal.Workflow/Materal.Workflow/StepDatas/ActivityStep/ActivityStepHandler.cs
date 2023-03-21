using Materal.Workflow.WorkflowCoreExtension.Extensions;
using Materal.Workflow.WorkflowCoreExtension.Models;
using System.Linq.Expressions;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 活动节点处理器
    /// </summary>
    public class ActivityStepHandler : BaseStepHandler<ActivityStepData>, IStepHandler<ActivityStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, ActivityStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Expression<Func<Dictionary<string, object?>, object>> expression = GetValueExpression(stepData);
            Expression<Func<Dictionary<string, object?>, DateTime>>? effectiveDate = null;
            if (stepData.ValidTime != null)
            {
                DateTime dateTime = DateTime.Now.Add(stepData.ValidTime.Value);
                effectiveDate = m => dateTime;
            }
            stepBuilder = InvokeMethodByMethodName(stepBuilder, nameof(Activity), new object?[] {
                            stepData.ActivityName,
                            expression,
                            effectiveDate,
                            null
                        }, new Type[] {
                            typeof(string),
                            expression.GetType(),
                            typeof(Expression<Func<Dictionary<string, object?>, DateTime>>),
                            typeof(Expression<Func<Dictionary<string, object?>, bool>>)
                        });
            ActivityOutputs(stepBuilder, stepData.Outputs);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
        /// <summary>
        /// 获得值表达式
        /// </summary>
        /// <param name="stepData"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        private Expression<Func<Dictionary<string, object?>, object>> GetValueExpression(ActivityStepData stepData)
        {
            ParameterExpression mValue = Expression.Parameter(typeof(Dictionary<string, object?>), "m");
            Expression tempExpression;
            switch (stepData.ValueySource)
            {
                case ValueSourceEnum.RuntimeDataProperty:
                    tempExpression = StepHandlerHelper.GetDictionaryIndexValueExpression(mValue, stepData.Value);
                    break;
                case ValueSourceEnum.BuildDataProperty:
                    object dataValue = stepData.GetBuildDataValue<object>(stepData.Value);
                    tempExpression = Expression.Constant(dataValue);
                    break;
                case ValueSourceEnum.Constant:
                    tempExpression = Expression.Constant(stepData.Value);
                    break;
                default:
                    throw new WorkflowException($"活动值来源错误");
            }
            tempExpression = Expression.Convert(tempExpression, typeof(object));
            tempExpression = Expression.Lambda(tempExpression, mValue);
            if (tempExpression is not Expression<Func<Dictionary<string, object?>, object>> result) throw new WorkflowException("值获取失败");
            return result;
        }
        /// <summary>
        /// 活动输出
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="outputs"></param>
        /// <returns></returns>
        private void ActivityOutputs(object stepBuilder, ICollection<OutputData>? outputs)
        {
            if (outputs == null || outputs.Count <= 0) return;
            Type stepBuilderType = stepBuilder.GetType();
            Type[] genericTypes = stepBuilderType.GenericTypeArguments;
            if (genericTypes.Length != 2) throw new WorkflowException("stepBuilder类型错误");
            Type stepType = genericTypes[1];
            if (stepType != typeof(Activity)) throw new WorkflowException("stepBuilder类型错误");
            foreach (OutputData outputData in outputs)
            {
                ActivityOutput(stepBuilder, outputData, stepType);
            }
        }
        /// <summary>
        /// 活动输出
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="outputData"></param>
        /// <param name="stepType"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        private void ActivityOutput(object stepBuilder, OutputData outputData, Type stepType)
        {
            #region value
            ParameterExpression mValue = Expression.Parameter(stepType, "m");
            Expression propertyExpression = Expression.Property(mValue, nameof(Activity.Result));
            Expression tempExpression;
            if (!string.IsNullOrWhiteSpace(outputData.StepProperty))
            {
                Expression valuePropertyExpression = Expression.Constant(outputData.StepProperty);
                tempExpression = Expression.Call(GetPropertyObjectValueMethodInfo, propertyExpression, valuePropertyExpression);
            }
            else
            {
                tempExpression = propertyExpression;
            }
            tempExpression = Expression.Convert(tempExpression, typeof(object));
            LambdaExpression value = Expression.Lambda(tempExpression, mValue);
            #endregion
            #region dataProperty
            ParameterExpression mDataProperty = Expression.Parameter(StepHandlerHelper.RuntimeDataType, "m");
            tempExpression = StepHandlerHelper.GetDictionaryIndexValueExpression(mDataProperty, outputData.RuntimeDataProperty);
            LambdaExpression dataProperty = Expression.Lambda(tempExpression, mDataProperty);
            #endregion
            WorkflowStep step = GetWorkflowStep(stepBuilder);
            step.Outputs.Add(new DictionaryMemberMapParameter(value, dataProperty));
        }
    }
}
