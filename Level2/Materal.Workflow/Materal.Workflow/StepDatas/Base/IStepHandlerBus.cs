using WorkflowCore.Interface;

namespace Materal.Workflow.StepDatas
{
    /// <summary>
    /// 节点处理器总线
    /// </summary>
    public interface IStepHandlerBus
    {
        /// <summary>
        /// 工作流构建器
        /// </summary>
        IWorkflowBuilder<Dictionary<string, object?>>? WorkflowBuilder { get; }
        /// <summary>
        /// 构建节点
        /// </summary>
        /// <param name="stepBuilder"></param>
        /// <param name="stepData"></param>
        /// <returns></returns>
        object BuildStep(object stepBuilder, IStepData stepData);
        /// <summary>
        /// 构建工作流
        /// </summary>
        /// <param name="workflowBuilder"></param>
        /// <param name="stepData"></param>
        /// <returns></returns>
        object BuildWorkflow(IWorkflowBuilder<Dictionary<string, object?>> workflowBuilder, IStepData stepData);
    }
}
