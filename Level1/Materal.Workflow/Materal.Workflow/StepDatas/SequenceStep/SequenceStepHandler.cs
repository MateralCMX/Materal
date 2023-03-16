namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 顺序节点处理器
    /// </summary>
    public class SequenceStepHandler : BaseStepHandler<SequenceStepData>, IStepHandler<SequenceStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, SequenceStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            foreach (IStepData subStepData in stepData.StepDatas)
            {
                stepBuilder = stepHandlerBus.BuildStep(stepBuilder, subStepData);
            }
            return stepBuilder;
        }
    }
}
