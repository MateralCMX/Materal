using Materal.Workflow.WorkflowCoreExtension.Extensions;
using System.Collections;
using System.Linq.Expressions;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 项循环节点处理器
    /// </summary>
    public class ForEachStepHandler : BaseStepHandler<ForEachStepData>, IStepHandler<ForEachStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, ForEachStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Expression<Func<Dictionary<string, object?>, IEnumerable>> expression = GetForEachExpression(stepData);
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "ForEach", new object?[] { expression });
            stepBuilder = Do(stepBuilder, stepData.StepData, stepHandlerBus);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
        /// <summary>
        /// 获得循环表达式
        /// </summary>
        /// <param name="stepData"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="WorkflowException"></exception>
        private Expression<Func<Dictionary<string, object?>, IEnumerable>> GetForEachExpression(ForEachStepData stepData)
        {
            Expression<Func<Dictionary<string, object?>, IEnumerable>> result;
            string stringValue = stepData.Value is string tempString ? tempString : stepData.Value.ToString();
            switch (stepData.ValueSource)
            {
                case ValueSourceEnum.RuntimeDataProperty:
                    result = m => (m[stringValue] as IEnumerable) ?? new bool[0];
                    break;
                case ValueSourceEnum.BuildDataProperty:
                    IEnumerable dataValue = stepData.GetBuildDataValue<IEnumerable>(stringValue);
                    result = m => dataValue;
                    break;
                case ValueSourceEnum.Constant:
                    if (stepData.Value is not IEnumerable enumerable) throw new WorkflowException("值类型错误");
                    result = m => enumerable;
                    break;
                default:
                    throw new WorkflowException("值类型错误");
            }
            return result;
        }
    }
}
