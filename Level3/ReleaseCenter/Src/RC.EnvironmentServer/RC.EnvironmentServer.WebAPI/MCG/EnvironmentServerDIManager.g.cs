#nullable enable
using Materal.BaseCore.Common;
using NetCore.AutoRegisterDi;
using RC.Core.WebAPI;
using RC.EnvironmentServer.Common;
using RC.EnvironmentServer.EFRepository;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RC.EnvironmentServer.WebAPI
{
    /// <summary>
    /// EnvironmentServer依赖注入管理器
    /// </summary>
    public partial class EnvironmentServerDIManager : DIManager
    {
        /// <summary>
        /// 添加EnvironmentServer服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddEnvironmentServerService(IServiceCollection services) => AddRCEnvironmentServerService(services);
    }
    /// <summary>
    /// 依赖注入管理器
    /// </summary>
    public abstract class DIManager
    {
        /// <summary>
        /// 添加EnvironmentServer服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddRCEnvironmentServerService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.EnvironmentServer.WebAPI.xml",
                $"{basePath}RC.EnvironmentServer.DataTransmitModel.xml",
                $"{basePath}RC.EnvironmentServer.PresentationModel.xml"
            };
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.AddRCService<EnvironmentServerDBContext>(ApplicationConfig.DBConfig, swaggerGenConfig, swaggerXmlPaths);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.EnvironmentServer.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.EnvironmentServer.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
