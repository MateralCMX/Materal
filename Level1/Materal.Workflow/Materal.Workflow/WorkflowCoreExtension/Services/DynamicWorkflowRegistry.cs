using Materal.Workflow.StepDatas;
using Materal.Workflow.WorkflowCoreExtension.Interface;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services;

namespace Materal.Workflow.WorkflowCoreExtension.Services
{
    /// <summary>
    /// 动态工作流登记处
    /// </summary>
    public class DynamicWorkflowRegistry : WorkflowRegistry, IDynamicWorkflowRegistry
    {
        private readonly IServiceProvider _services;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="services"></param>
        public DynamicWorkflowRegistry(IServiceProvider services) : base(services)
        {
            _services = services;
        }
        /// <summary>
        /// 获得动态工作流定义
        /// </summary>
        /// <param name="stepData"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        public WorkflowDefinition GetDynamicWorkflowDefinition(StartStepData stepData)
        {
            IWorkflowBuilder<Dictionary<string, object?>>? builder = _services.GetService<IWorkflowBuilder>()?.UseData<Dictionary<string,object?>>();
            if (builder == null) throw new WorkflowException($"获取{nameof(IWorkflowBuilder)}失败");
            DynamicWorkFlow dynamicWorkFlow = ActivatorUtilities.CreateInstance<DynamicWorkFlow>(_services);
            dynamicWorkFlow.Build(builder, stepData);
            WorkflowDefinition result = builder.Build($"{dynamicWorkFlow.Id}{Guid.NewGuid()}", dynamicWorkFlow.Version);
            RegisterWorkflow(result);
            return result;
        }
    }
}
