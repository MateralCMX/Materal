using Materal.BaseCore.Common;
using NetCore.AutoRegisterDi;
using RC.Core.WebAPI;
using RC.Deploy.Common;
using RC.Deploy.EFRepository;
using System.Reflection;

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
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.Deploy.WebAPI.xml",
                $"{basePath}RC.Deploy.DataTransmitModel.xml",
                $"{basePath}RC.Deploy.PresentationModel.xml",
                $"{basePath}RC.Deploy.Hubs.xml"
            };
            services.AddRCService<DeployDBContext>(ApplicationConfig.DBConfig, config => 
            {
                config.AddSignalRSwaggerGen(option =>
                {
                    option.ScanAssembly(Assembly.Load("RC.Deploy.Hubs"));
                });
            }, swaggerXmlPaths);
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Deploy.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Deploy.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            return services;
        }
    }
}
