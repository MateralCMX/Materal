using NetCore.AutoRegisterDi;
using RC.Core;
using RC.Core.WebAPI;
using RC.Deploy.Common;
using System.Reflection;
using RC.Deploy.RepositoryImpl;

namespace RC.Deploy
{
    /// <summary>
    /// Deploy依赖注入扩展
    /// </summary>
    public static class DeployDIExtension
    {
        /// <summary>
        /// 添加Deploy服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDeployService(this IServiceCollection services)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}{currentAssembly.GetName().Name}.xml",
            };
            services.AddWebAPIServices<DeployDBContext>(swaggerXmlPaths, ApplicationConfig.DBConfig);
            services.AddRCServices(currentAssembly);
            services.RegisterAssemblyPublicNonGenericClasses(currentAssembly)
                .Where(m => !m.IsAbstract && (m.Name.EndsWith("RepositoryImpl") || m.Name.EndsWith("ServiceImpl")))
                .AsPublicImplementedInterfaces();
            services.AddIntegrationEventBus("RCDeployQueue");
            services.AddIntegrationEventHandlers(currentAssembly);
            return services;
        }
    }
}
