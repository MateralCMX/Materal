using Materal.Workflow.WorkflowCoreExtension.Extensions;
using Materal.Workflow.WorkflowCoreExtension.Models;
using System.Linq.Expressions;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 事件节点处理器
    /// </summary>
    public class EventStepHandler : BaseStepHandler<EventStepData>, IStepHandler<EventStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, EventStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Expression<Func<Dictionary<string, object?>, string>> expression = GetEventKeyExpression(stepData);
            Expression<Func<Dictionary<string, object?>, DateTime>>? effectiveDate = null;
            if (stepData.ValidTime != null)
            {
                DateTime dateTime = DateTime.Now.Add(stepData.ValidTime.Value);
                effectiveDate = m => dateTime;
            }
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "WaitFor", new object?[] {
                stepData.EventName,
                expression,
                effectiveDate
            }, new Type[] {
                typeof(string),
                expression.GetType(),
                typeof(Expression<Func<Dictionary<string, object?>, DateTime>>),
                typeof(Expression<Func<Dictionary<string, object?>, bool>>)
            });
            EventOutputs(stepBuilder, stepData.Outputs);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
        /// <summary>
        /// 获得事件Key表达式
        /// </summary>
        /// <param name="stepData"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        private Expression<Func<Dictionary<string, object?>, string>> GetEventKeyExpression(EventStepData stepData)
        {
            ParameterExpression mValue = Expression.Parameter(typeof(Dictionary<string, object?>), "m");
            Expression tempExpression;
            switch (stepData.EventKeySource)
            {
                case ValueSourceEnum.RuntimeDataProperty:
                    tempExpression = StepHandlerHelper.GetDictionaryIndexValueExpression(mValue, stepData.EventKey);
                    tempExpression = Expression.Convert(tempExpression, typeof(string));
                    break;
                case ValueSourceEnum.BuildDataProperty:
                    object dataValue = stepData.GetBuildDataValue<object>(stepData.EventKey);
                    tempExpression = Expression.Constant(dataValue);
                    break;
                case ValueSourceEnum.Constant:
                    tempExpression = Expression.Constant(stepData.EventKey);
                    break;
                default:
                    throw new WorkflowException("事件Key来源错误");
            }
            tempExpression = Expression.Lambda(tempExpression, mValue);
            if (tempExpression is not Expression<Func<Dictionary<string, object?>, string>> result) throw new WorkflowException("事件Key获取失败");
            return result;
        }
        /// <summary>
        /// 事件输出
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="outputs"></param>
        /// <returns></returns>
        private void EventOutputs(object stepBuilder, ICollection<OutputData>? outputs)
        {
            if (outputs == null || outputs.Count <= 0) return;
            Type stepBuilderType = stepBuilder.GetType();
            Type[] genericTypes = stepBuilderType.GenericTypeArguments;
            if (genericTypes.Length != 2) throw new WorkflowException("stepBuilder类型错误");
            Type runtimeDataType = genericTypes[0];
            Type stepType = genericTypes[1];
            if (stepType != typeof(WaitFor)) throw new WorkflowException("stepBuilder类型错误");
            foreach (OutputData outputData in outputs)
            {
                EventOutput(stepBuilder, outputData, runtimeDataType, stepType);
            }
        }
        /// <summary>
        /// 事件输出
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="outputData"></param>
        /// <param name="runtimeDataType"></param>
        /// <param name="stepType"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        private void EventOutput(object stepBuilder, OutputData outputData, Type runtimeDataType, Type stepType)
        {
            #region value
            ParameterExpression mValue = Expression.Parameter(stepType, "m");
            Expression propertyExpression = Expression.Property(mValue, nameof(WaitFor.EventData));
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
            ParameterExpression mDataProperty = Expression.Parameter(runtimeDataType, "n");
            tempExpression = StepHandlerHelper.GetDictionaryIndexValueExpression(mDataProperty, outputData.RuntimeDataName);
            LambdaExpression dataProperty = Expression.Lambda(tempExpression, mDataProperty);
            #endregion
            WorkflowStep step = GetWorkflowStep(stepBuilder);
            step.Outputs.Add(new DictionaryMemberMapParameter(value, dataProperty));
        }
    }
}
