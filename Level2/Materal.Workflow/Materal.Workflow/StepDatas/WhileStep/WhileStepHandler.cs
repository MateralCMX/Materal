using System.Linq.Expressions;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 条件循环节点处理器
    /// </summary>
    public class WhileStepHandler : BaseStepHandler<WhileStepData>, IStepHandler<WhileStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, WhileStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Expression<Func<Dictionary<string, object?>, bool>> decisionExpression = GetDecisionConditionExpression(stepData.Conditions, stepData);
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "While", new object?[] { decisionExpression });
            stepBuilder = Do(stepBuilder, stepData.StepData, stepHandlerBus);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
    }
}
