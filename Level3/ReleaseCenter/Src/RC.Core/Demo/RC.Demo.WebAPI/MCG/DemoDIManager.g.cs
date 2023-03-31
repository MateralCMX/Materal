#nullable enable
using Materal.BaseCore.Common;
using NetCore.AutoRegisterDi;
using RC.Core.WebAPI;
using RC.Demo.Common;
using RC.Demo.EFRepository;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RC.Demo.WebAPI
{
    /// <summary>
    /// Demo依赖注入管理器
    /// </summary>
    public partial class DemoDIManager : DIManager
    {
        /// <summary>
        /// 添加Demo服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddDemoService(IServiceCollection services) => AddRCDemoService(services);
    }
    /// <summary>
    /// 依赖注入管理器
    /// </summary>
    public abstract class DIManager
    {
        /// <summary>
        /// 添加Demo服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddRCDemoService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.Demo.WebAPI.xml",
                $"{basePath}RC.Demo.DataTransmitModel.xml",
                $"{basePath}RC.Demo.PresentationModel.xml"
            };
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.AddRCService<DemoDBContext>(ApplicationConfig.DBConfig, swaggerGenConfig, swaggerXmlPaths);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Demo.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Demo.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
