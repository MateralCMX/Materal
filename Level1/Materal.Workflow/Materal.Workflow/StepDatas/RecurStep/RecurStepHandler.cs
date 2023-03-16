using System.Linq.Expressions;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 重复节点处理器
    /// </summary>
    public class RecurStepHandler : BaseStepHandler<RecurStepData>, IStepHandler<RecurStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, RecurStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Expression<Func<Dictionary<string, object?>, TimeSpan>> expression = m => stepData.Interval;
            Expression<Func<Dictionary<string, object?>, bool>> decisionExpression = GetDecisionConditionExpression(stepData.Conditions, stepData);
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "Recur", new object?[] { expression, decisionExpression });
            stepBuilder = Do(stepBuilder, stepData.StepData, stepHandlerBus);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
    }
}
