using Materal.BaseCore.WebAPI;
using NetCore.AutoRegisterDi;
using RC.Core;
using RC.Core.EFRepository;
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
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}{currentAssembly.GetName().Name}.xml",
            };
            services.AddDBService<DemoDBContext>(DemoConfig.DBConfig);
            services.AddWebAPIService(swaggerXmlPaths);
            services.AddMateralCoreServices(currentAssembly);
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
