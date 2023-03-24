using Materal.Workflow.StepBodys;
using WorkflowCore.Interface;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 事务节点处理器
    /// </summary>
    public class TransactionStepHandler : BaseStepHandler<TransactionStepData>, IStepHandler<TransactionStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, TransactionStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            Action<IWorkflowBuilder<Dictionary<string, object?>>> innerBuilder = m =>
            {
                object newStepBuilder = m.StartWith<EmptyStepBody>();
                stepHandlerBus.BuildStep(newStepBuilder, stepData.StepData);
            };
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "Saga", new object?[] { innerBuilder });
            if (stepData.CompensateStep != null)
            {
                stepBuilder = CompensateWith(stepBuilder, stepData.CompensateStep, stepHandlerBus);
            }
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
    }
}
