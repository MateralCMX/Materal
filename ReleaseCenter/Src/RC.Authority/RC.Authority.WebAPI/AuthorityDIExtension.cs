using NetCore.AutoRegisterDi;
using RC.Authority.Common;
using RC.Authority.EFRepository;
using RC.Core;
using RC.Core.WebAPI;
using System.Reflection;

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
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.Authority.WebAPI.xml",
                $"{basePath}RC.Authority.DataTransmitModel.xml",
                $"{basePath}RC.Authority.PresentationModel.xml",
            };
            services.AddRCService<AuthorityDBContext>(ApplicationConfig.DBConfig, swaggerXmlPaths);
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Authority.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Authority.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();
            return services;
        }
    }
}
