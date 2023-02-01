using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Common;
using NetCore.AutoRegisterDi;
using RC.Core.WebAPI;
using RC.ServerCenter.Common;
using RC.ServerCenter.EFRepository;
using System.Reflection;

namespace RC.ServerCenter
{
    /// <summary>
    /// ServerCenter依赖注入扩展
    /// </summary>
    public static class ServerCenterDIExtension
    {
        /// <summary>
        /// 添加ServerCenter服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServerCenterService(this IServiceCollection services)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.ServerCenter.WebAPI.xml",
                $"{basePath}RC.ServerCenter.DataTransmitModel.xml",
                $"{basePath}RC.ServerCenter.PresentationModel.xml",
            };
            services.AddRCService<ServerCenterDBContext>(ApplicationConfig.DBConfig, swaggerXmlPaths);
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.ServerCenter.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.ServerCenter.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            services.AddIntegrationEventBus($"{WebAPIConfig.AppName}Queue");
            return services;
        }
    }
}
