using Materal.Workflow.StepDatas;
using Materal.Workflow.WorkflowCoreExtension.Interface;
using Materal.Workflow.WorkflowCoreExtension.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Materal.Workflow
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        /// <summary>
        /// 添加动态工作流服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <param name="stepHandlerAssemblys"></param>
        /// <returns></returns>
        /// <exception cref="WorkflowException"></exception>
        public static IServiceCollection AddDynamicWorkflowService(this IServiceCollection services, Action<WorkflowOptions>? setupAction = null, params Assembly[] stepHandlerAssemblys)
        {
            services.AddWorkflow(setupAction);
            services.TryAddSingleton(typeof(IStepHandlerBus), typeof(StepHandlerBus));
            List<Assembly> stepHandlerAssemblyList =  stepHandlerAssemblys.ToList();
            stepHandlerAssemblyList.Add(Assembly.Load("Materal.Workflow"));
            StepHandlerBusHelper.AddStepHandlers(stepHandlerAssemblyList);
            services.AddSingleton<IDynamicWorkflowHost, DynamicWorkflowHost>();
            services.Replace(new ServiceDescriptor(typeof(IWorkflowHost), s => s.GetService<IDynamicWorkflowHost>() ?? throw new WorkflowException("获取服务失败"), ServiceLifetime.Singleton));
            services.AddSingleton<IDynamicWorkflowController, DynamicWorkflowController>();
            services.Replace(new ServiceDescriptor(typeof(IWorkflowController), s => s.GetService<IDynamicWorkflowController>() ?? throw new WorkflowException("获取服务失败"), ServiceLifetime.Singleton));
            services.AddSingleton<IDynamicWorkflowRegistry, DynamicWorkflowRegistry>();
            services.Replace(new ServiceDescriptor(typeof(IWorkflowRegistry), s => s.GetService<IDynamicWorkflowRegistry>() ?? throw new WorkflowException("获取服务失败"), ServiceLifetime.Singleton));
            return services;
        }
    }
}

