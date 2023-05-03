#nullable enable
using Materal.BaseCore.Common;
using NetCore.AutoRegisterDi;
using RC.Core.WebAPI;
using RC.ServerCenter.Common;
using RC.ServerCenter.EFRepository;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RC.ServerCenter.WebAPI
{
    /// <summary>
    /// ServerCenter依赖注入管理器
    /// </summary>
    public partial class ServerCenterDIManager : DIManager
    {
        /// <summary>
        /// 添加ServerCenter服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddServerCenterService(IServiceCollection services) => AddRCServerCenterService(services);
    }
    /// <summary>
    /// 依赖注入管理器
    /// </summary>
    public abstract class DIManager
    {
        /// <summary>
        /// 添加ServerCenter服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <returns></returns>
        public virtual IServiceCollection AddRCServerCenterService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string[] swaggerXmlPaths = new[]
            {
                $"{basePath}RC.ServerCenter.WebAPI.xml",
                $"{basePath}RC.ServerCenter.DataTransmitModel.xml",
                $"{basePath}RC.ServerCenter.PresentationModel.xml"
            };
            services.AddMateralCoreServices(Assembly.GetExecutingAssembly());
            services.AddRCService<ServerCenterDBContext>(ApplicationConfig.DBConfig, swaggerGenConfig, swaggerXmlPaths);
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("RC.ServerCenter.ServiceImpl"))
                .Where(m => !m.IsAbstract && m.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            return services;
        }
    }
}
