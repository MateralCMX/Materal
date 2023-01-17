using Materal.BaseCore.Common;
using Materal.BaseCore.HttpClient.Extensions;
using Materal.BaseCore.WebAPI.Common;
using NetCore.AutoRegisterDi;
using RC.Core.WebAPI;
using RC.EnvironmentServer.Common;
using RC.EnvironmentServer.EFRepository;
using System.Reflection;

namespace RC.EnvironmentServer.WebAPI
{
    /// <summary>
    /// EnvironmentServer依赖注入扩展
    /// </summary>
    public static class EnvironmentServerDIExtension
    {
        /// <summary>
        /// 添加EnvironmentServer服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEnvironmentServerService(this IServiceCollection services)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.EnvironmentServer.WebAPI.xml",
                $"{basePath}RC.EnvironmentServer.DataTransmitModel.xml",
                $"{basePath}RC.EnvironmentServer.PresentationModel.xml",
            };
            services.AddRCService<EnvironmentServerDBContext>(ApplicationConfig.DBConfig, swaggerXmlPaths);
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.EnvironmentServer.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.EnvironmentServer.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.AddHttpClientService(WebAPIConfig.AppName, Assembly.Load("RC.ServerCenter.HttpClient"));
            services.AddIntegrationEventBus($"{WebAPIConfig.AppName}Queue");
            services.AddIntegrationEventHandlers(Assembly.Load("RC.EnvironmentServer.IntegrationEventHandlers"));
            return services;
        }
    }
}
