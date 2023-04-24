using Materal.Workflow.StepDatas;
using Materal.Workflow.WorkflowCoreExtension.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Models.LifeCycleEvents;
using WorkflowCore.Services;

namespace Materal.Workflow.WorkflowCoreExtension.Services
{
    /// <summary>
    /// 动态工作流控制器
    /// </summary>
    public class DynamicWorkflowController : WorkflowController, IDynamicWorkflowController
    {
        private readonly IDynamicWorkflowRegistry _registry;
        private readonly IPersistenceProvider _persistenceStore;
        private readonly IQueueProvider _queueProvider;
        private readonly IExecutionPointerFactory _pointerFactory;
        private readonly ILifeCycleEventHub _eventHub;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DynamicWorkflowController(IPersistenceProvider persistenceStore, IDistributedLockProvider lockProvider, IDynamicWorkflowRegistry registry, IQueueProvider queueProvider, IExecutionPointerFactory pointerFactory, ILifeCycleEventHub eventHub, ILoggerFactory loggerFactory, IServiceProvider services, IDateTimeProvider dateTimeProvider) : base(persistenceStore, lockProvider, registry, queueProvider, pointerFactory, eventHub, loggerFactory, services, dateTimeProvider)
        {
            _persistenceStore = persistenceStore;
            _registry = registry;
            _queueProvider = queueProvider;
            _pointerFactory = pointerFactory;
            _eventHub = eventHub;
            _serviceProvider = services;
            _dateTimeProvider = dateTimeProvider;
        }
        /// <summary>
        /// 启动动态工作流
        /// </summary>
        /// <param name="stepData"></param>
        /// <param name="runtimeData"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        public async Task<string> StartDynamicWorkflow(StartStepData stepData, Dictionary<string, object?>? runtimeData = null, string? reference = null)
        {
            WorkflowDefinition workflowDefinition = _registry.GetDynamicWorkflowDefinition(stepData);
            WorkflowInstance workflowInstance = new()
            {
                WorkflowDefinitionId = workflowDefinition.Id,
                Version = workflowDefinition.Version,
                Data = runtimeData,
                Description = workflowDefinition.Description,
                NextExecution = 0,
                CreateTime = _dateTimeProvider.UtcNow,
                Status = WorkflowStatus.Runnable,
                Reference = reference
            };
            if ((workflowDefinition.DataType != null) && (runtimeData == null))
            {
                if (typeof(Dictionary<string, object?>) == workflowDefinition.DataType)
                {
                    workflowInstance.Data = new Dictionary<string, object?>();
                }
                else
                {
                    workflowInstance.Data = workflowDefinition.DataType.GetConstructor(new Type[0]).Invoke(new object[0]);
                }
            }
            workflowInstance.ExecutionPointers.Add(_pointerFactory.BuildGenesisPointer(workflowDefinition));
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IWorkflowMiddlewareRunner middlewareRunner = scope.ServiceProvider.GetRequiredService<IWorkflowMiddlewareRunner>();
                await middlewareRunner.RunPreMiddleware(workflowInstance, workflowDefinition);
            }
            string id = await _persistenceStore.CreateNewWorkflow(workflowInstance);
            await _queueProvider.QueueWork(id, QueueType.Workflow);
            await _queueProvider.QueueWork(id, QueueType.Index);
            await _eventHub.PublishNotification(new WorkflowStarted
            {
                EventTimeUtc = _dateTimeProvider.UtcNow,
                Reference = reference,
                WorkflowInstanceId = id,
                WorkflowDefinitionId = workflowDefinition.Id,
                Version = workflowDefinition.Version
            });
            return id;
        }
        /// <summary>
        /// 启动动态工作流
        /// </summary>
        /// <typeparam name="TRuntimeData"></typeparam>
        /// <param name="stepData"></param>
        /// <param name="runtimeData"></param>
        /// <param name="reference"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<string> StartDynamicWorkflow<TRuntimeData>(StartStepData stepData, TRuntimeData? runtimeData = null, string? reference = null) where TRuntimeData : class, new()
        {
            Dictionary<string, object?>? trueRuntimeData = null;
            if (runtimeData != null)
            {
                trueRuntimeData = new Dictionary<string, object?>();
                Type runtimeType = runtimeData.GetType();
                foreach (PropertyInfo propertyInfo in runtimeType.GetProperties())
                {
                    trueRuntimeData.Add(propertyInfo.Name, propertyInfo.GetValue(runtimeData, null));
                }
            }
            return StartDynamicWorkflow(stepData, trueRuntimeData, reference);
        }
    }
}
