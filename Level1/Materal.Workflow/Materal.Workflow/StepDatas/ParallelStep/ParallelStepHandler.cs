namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 并行节点处理器
    /// </summary>
    public class ParallelStepHandler : BaseStepHandler<ParallelStepData>, IStepHandler<ParallelStepData>
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object BuildStep(object stepBuilder, ParallelStepData stepData, IStepHandlerBus stepHandlerBus)
        {
            stepBuilder = Parallel(stepBuilder, stepData.StepDatas, stepHandlerBus);
            stepBuilder = Next(stepBuilder, stepData.Next, stepHandlerBus);
            return stepBuilder;
        }
        /// <summary>
        /// 并行
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepDatas"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        private object Parallel(object stepBuilder, List<IStepData> stepDatas, IStepHandlerBus stepHandlerBus)
        {
            if (stepDatas.Count <= 0) return stepBuilder;
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "Parallel");
            foreach (IStepData stepData in stepDatas)
            {
                stepBuilder = Do(stepBuilder, stepData, stepHandlerBus);
            }
            stepBuilder = InvokeMethodByMethodName(stepBuilder, "Join");
            return stepBuilder;
        }
    }
}
