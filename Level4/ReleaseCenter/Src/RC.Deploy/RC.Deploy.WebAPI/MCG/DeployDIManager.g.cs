#nullable enable
using Materal.BaseCore.Common;
using NetCore.AutoRegisterDi;
using RC.Core.WebAPI;
using RC.Deploy.Common;
using RC.Deploy.EFRepository;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RC.Deploy.WebAPI
{
    /// <summary>
    /// Deploy依赖注入管理器
    /// </summary>
    public partial class DeployDIManager : DIManager
    {
        /// <summary>
        /// 添加Deploy服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddDeployService(IServiceCollection services) => AddRCDeployService(services);
    }
    /// <summary>
    /// 依赖注入管理器
    /// </summary>
    public abstract class DIManager
    {
        /// <summary>
        /// 添加Deploy服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddRCDeployService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.Deploy.WebAPI.xml",
                $"{basePath}RC.Deploy.DataTransmitModel.xml",
                $"{basePath}RC.Deploy.PresentationModel.xml"
            };
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.AddRCService<DeployDBContext>(ApplicationConfig.DBConfig, swaggerGenConfig, swaggerXmlPaths);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Deploy.EFRepository"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.Deploy.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
