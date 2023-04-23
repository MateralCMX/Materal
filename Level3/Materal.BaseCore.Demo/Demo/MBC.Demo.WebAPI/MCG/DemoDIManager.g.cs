#nullable enable
using Materal.BaseCore.Common;
using NetCore.AutoRegisterDi;
using MBC.Core.WebAPI;
using MBC.Demo.Common;
using MBC.Demo.EFRepository;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MBC.Demo.WebAPI
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
        public virtual IServiceCollection AddDemoService(IServiceCollection services) => AddMBCDemoService(services);
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
        public virtual IServiceCollection AddMBCDemoService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}MBC.Demo.WebAPI.xml",
                $"{basePath}MBC.Demo.DataTransmitModel.xml",
                $"{basePath}MBC.Demo.PresentationModel.xml"
            };
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.AddMBCService<DemoDBContext>(ApplicationConfig.DBConfig, swaggerGenConfig, swaggerXmlPaths);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("MBC.Demo.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("MBC.Demo.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
