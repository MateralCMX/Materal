using Materal.BaseCore.Common;
using Materal.BaseCore.WebAPI.Common;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RC.ServerCenter.WebAPI
{
    public partial class ServerCenterDIManager
    {
        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerGenConfig"></param>
        /// <returns></returns>
        public override IServiceCollection AddRCServerCenterService(IServiceCollection services, Action<SwaggerGenOptions>? swaggerGenConfig = null)
        {
            base.AddRCServerCenterService(services, swaggerGenConfig);
            services.AddIntegrationEventBus($"{WebAPIConfig.AppName}Queue");
            return services;
        }
    }
}
