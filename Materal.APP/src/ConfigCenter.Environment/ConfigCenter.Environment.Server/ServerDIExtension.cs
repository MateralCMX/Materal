using ConfigCenter.Environment.DependencyInjection;
using Materal.APP.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Materal.APP.TFMS.Core;

namespace ConfigCenter.Environment.Server
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServerDIExtension
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddConfigCenterEnvironmentServerServices(this IServiceCollection services)
        {
            services.AddConfigCenterEnvironmentServices();
            services.AddIntegrationEventHandlers(Assembly.Load("ConfigCenter.Environment.IntegrationEventHandlers"));
            services.AddAutoMapperService(Assembly.Load("ConfigCenter.Environment.IntegrationEventHandlers"), Assembly.Load("ConfigCenter.Environment.ServiceImpl"), Assembly.Load("ConfigCenter.Environment.Server"));
        }
    }
}
