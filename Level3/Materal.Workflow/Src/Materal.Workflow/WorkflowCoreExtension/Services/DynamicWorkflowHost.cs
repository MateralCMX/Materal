using Materal.Workflow.WorkflowCoreExtension.Interface;
using Materal.Workflow.StepDatas;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services;

namespace Materal.Workflow.WorkflowCoreExtension.Services
{
    /// <summary>
    /// 动态工作流主机
    /// </summary>
    public class DynamicWorkflowHost : WorkflowHost, IDynamicWorkflowHost
    {
        private readonly IDynamicWorkflowController _workflowController;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DynamicWorkflowHost(IPersistenceProvider persistenceStore, IQueueProvider queueProvider, WorkflowOptions options, ILoggerFactory loggerFactory, IServiceProvider serviceProvider, IWorkflowRegistry registry, IDistributedLockProvider lockProvider, IEnumerable<IBackgroundTask> backgroundTasks, IDynamicWorkflowController workflowController, ILifeCycleEventHub lifeCycleEventHub, ISearchIndex searchIndex, IActivityController activityController) : base(persistenceStore, queueProvider, options, loggerFactory, serviceProvider, registry, lockProvider, backgroundTasks, workflowController, lifeCycleEventHub, searchIndex, activityController)
        {
            _workflowController = workflowController;
        }
        /// <summary>
        /// 启动动态工作流
        /// </summary>
        /// <typeparam name="TRuntimeData"></typeparam>
        /// <param name="stepData"></param>
        /// <param name="runtimeData"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public Task<string> StartDynamicWorkflow<TRuntimeData>(StartStepData stepData, TRuntimeData? runtimeData, string? reference)
            where TRuntimeData : class, new() => _workflowController.StartDynamicWorkflow(stepData, runtimeData, reference);
        /// <summary>
        /// 启动工作流
        /// </summary>
        /// <param name="stepData"></param>
        /// <param name="runtimeData"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public Task<string> StartDynamicWorkflow(StartStepData stepData, Dictionary<string, object?>? runtimeData = null, string? reference = null)
            => _workflowController.StartDynamicWorkflow(stepData, runtimeData, reference);
    }
}
