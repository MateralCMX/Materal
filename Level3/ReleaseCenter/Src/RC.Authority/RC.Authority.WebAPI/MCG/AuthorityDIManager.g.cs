#nullable enable
using Materal.BaseCore.Common;
using NetCore.AutoRegisterDi;
using RC.Core.WebAPI;
using RC.Authority.Common;
using RC.Authority.EFRepository;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RC.Authority.WebAPI
{
    /// <summary>
    /// Authority依赖注入管理器
    /// </summary>
    public partial class AuthorityDIManager : DIManager
    {
        /// <summary>
        /// 添加Authority服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddAuthorityService(IServiceCollection services) => AddRCAuthorityService(services);
    }
    /// <summary>
    /// 依赖注入管理器
    /// </summary>
    public abstract class DIManager
    {
        /// <summary>
        /// 添加Authority服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddRCAuthorityService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.Authority.WebAPI.xml",
                $"{basePath}RC.Authority.DataTransmitModel.xml",
                $"{basePath}RC.Authority.PresentationModel.xml"
            };
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.AddRCService<AuthorityDBContext>(ApplicationConfig.DBConfig, swaggerGenConfig, swaggerXmlPaths);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Authority.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Authority.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
