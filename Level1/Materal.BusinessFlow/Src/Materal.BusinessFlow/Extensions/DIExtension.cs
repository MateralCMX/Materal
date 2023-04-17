using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.AutoNodes;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.AutoNodes.Base;
using Materal.BusinessFlow.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Materal.BusinessFlow.Extensions
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        public static IServiceCollection AddBusinessFlow(this IServiceCollection services, params Assembly[] autoNodeAssemblies)
        {
            services.TryAddScoped<IDataModelFieldService, DataModelFieldServiceImpl>();
            services.TryAddScoped<IDataModelService, DataModelServiceImpl>();
            services.TryAddScoped<IFlowTemplateService, FlowTemplateServiceImpl>();
            services.TryAddScoped<INodeService, NodeServiceImpl>();
            services.TryAddScoped<IStepService, StepServiceImpl>();
            services.TryAddScoped<IUserService, UserServiceImpl>();
            services.TryAddScoped<IBusinessFlowHost, BusinessFlowHostImpl>();
            services.TryAddSingleton<IAutoNodeBus, AutoNodeBusImpl>();
            services.AddScoped<BusinessFlowHelper>();
            List<Assembly> autoNodeAssemblyList = new()
            {
                Assembly.Load("Materal.BusinessFlow")
            };
            autoNodeAssemblyList.AddRange(autoNodeAssemblies);
            AutoNodeBusImpl.RegisterAutoNodes(services, autoNodeAssemblyList.ToArray());
            return services;
        }
    }
}
