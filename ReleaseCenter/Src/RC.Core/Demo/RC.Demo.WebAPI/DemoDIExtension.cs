using NetCore.AutoRegisterDi;
using RC.Core;
using RC.Core.WebAPI;
using RC.Demo.Common;
using RC.Demo.EFRepository;
using System.Reflection;

namespace RC.Demo.WebAPI
{
    /// <summary>
    /// Authority依赖注入扩展
    /// </summary>
    public static class DemoDIExtension
    {
        /// <summary>
        /// 添加Authority服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDemoService(this IServiceCollection services)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.Demo.WebAPI.xml",
                $"{basePath}RC.Demo.DataTransmitModel.xml",
                $"{basePath}RC.Demo.PresentationModel.xml"
            };
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.AddRCService<DemoDBContext>(ApplicationConfig.DBConfig, swaggerXmlPaths);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Demo.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Demo.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            return services;
        }
    }
}
