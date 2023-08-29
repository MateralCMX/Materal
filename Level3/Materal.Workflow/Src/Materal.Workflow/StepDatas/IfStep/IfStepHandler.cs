using System.Linq.Expressions;
using WorkflowCore.Primitives;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 条件节点数据
    /// </summary>
    public class IfStepHandler : BaseStepHandler<IfStepData>, IStepHandler<IfStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, IfStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Expression<Func<Dictionary<string, object?>, bool>> decisionExpression = GetDecisionConditionExpression(stepData.Conditions, stepData);
            stepBuilder = InvokeMethodByMethodName(stepBuilder, nameof(If), new object?[] { decisionExpression });
            stepBuilder = Do(stepBuilder, stepData.StepData, stepHandlerBus);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
    }
}
