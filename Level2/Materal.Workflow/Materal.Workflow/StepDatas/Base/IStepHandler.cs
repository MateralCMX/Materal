namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 节点处理器
    /// </summary>
    public interface IStepHandler
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        object Build(object stepBuilder, IStepData stepData, IStepHandlerBus stepHandlerBus);
    }
    /// <summary>
    /// 节点处理器
    /// </summary>
    /// <typeparam name="TStepData"></typeparam>
    public interface IStepHandler<TStepData> : IStepHandler
        where TStepData : class, IStepData
    {
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <param name="stepHandlerBus"></param>
        /// <returns></returns>
        object BuildStep(object stepBuilder, TStepData stepData, IStepHandlerBus stepHandlerBus);
    }
}
