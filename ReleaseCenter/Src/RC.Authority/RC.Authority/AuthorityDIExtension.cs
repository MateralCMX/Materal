using NetCore.AutoRegisterDi;
using RC.Core;
using RC.Core.WebAPI;
using RC.Authority.Common;
using System.Reflection;
using RC.Authority.RepositoryImpl;

namespace RC.Authority
{
    /// <summary>
    /// Authority依赖注入扩展
    /// </summary>
    public static class AuthorityDIExtension
    {
        /// <summary>
        /// 添加Authority服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorityService(this IServiceCollection services)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}{currentAssembly.GetName().Name}.xml",
            };
            services.AddWebAPIServices<AuthorityDBContext>(swaggerXmlPaths, ApplicationConfig.DBConfig);
            services.AddRCServices(currentAssembly);
            services.RegisterAssemblyPublicNonGenericClasses(currentAssembly)
                .Where(m => !m.IsAbstract && (m.Name.EndsWith("RepositoryImpl") || m.Name.EndsWith("ServiceImpl")))
                .AsPublicImplementedInterfaces();
            services.AddIntegrationEventBus("RCAuthorityQueue");
            services.AddIntegrationEventHandlers(currentAssembly);
            return services;
        }
    }
}
