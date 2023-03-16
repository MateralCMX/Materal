using Materal.Workflow.StepDatas;
using WorkflowCore.Interface;

namespace Materal.Workflow.WorkflowCoreExtension.Interface
{
    /// <summary>
    /// 动态工作流控制器
    /// </summary>
    public interface IDynamicWorkflowController : IWorkflowController
    {
        /// <summary>
        /// 启动动态工作流
        /// </summary>
        /// <typeparam name="TRuntimeData"></typeparam>
        /// <param name="stepData"></param>
        /// <param name="runtimeData"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        Task<string> StartDynamicWorkflow<TRuntimeData>(StartStepData stepData, TRuntimeData? runtimeData = null, string? reference = null)
            where TRuntimeData : class, new();
        /// <summary>
        /// 启动动态工作流
        /// </summary>
        /// <param name="stepData"></param>
        /// <param name="runtimeData"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        Task<string> StartDynamicWorkflow(StartStepData stepData, Dictionary<string, object?>? runtimeData = null, string? reference = null);
    }
}
