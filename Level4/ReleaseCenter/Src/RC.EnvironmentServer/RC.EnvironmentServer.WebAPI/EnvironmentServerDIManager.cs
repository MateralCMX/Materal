using Materal.BaseCore.Common;
using Materal.BaseCore.HttpClient.Extensions;
using Materal.BaseCore.WebAPI.Common;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace RC.EnvironmentServer.WebAPI
{
    public partial class EnvironmentServerDIManager
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <returns></returns>
        public override IServiceCollection AddRCEnvironmentServerService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            base.AddRCEnvironmentServerService(services, swaggerGenConfig);
            services.AddHttpClientService(WebAPIConfig.AppName, Assembly.Load("RC.ServerCenter.HttpClient"));
            services.AddIntegrationEventBus($"{WebAPIConfig.AppName}Queue");
            services.AddIntegrationEventHandlers(Assembly.Load("RC.EnvironmentServer.IntegrationEventHandlers"));
            return services;
        }
    }
}
