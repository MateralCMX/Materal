namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 结束节点处理器
    /// </summary>
    public class EndStepHandler : BaseStepHandler<EndStepData>, IStepHandler<EndStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, EndStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "EndWorkflow");
            return stepBuilder;
        }
    }
}
