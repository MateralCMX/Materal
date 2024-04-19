using Materal.Gateway.OcelotConsulExtension;
using Materal.Gateway.OcelotExtension.Repositories;
using Materal.Gateway.Service;
using Ocelot.DependencyInjection;

namespace Materal.Gateway
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加Materal网关
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddMateralGateway(this IServiceCollection services)
        {
            services.TryAddSingleton<IOcelotConfigRepository, OcelotConfigRepositoryImpl>();
            services.TryAddSingleton<IOcelotConfigService, OcelotConfigServiceImpl>();
            IOcelotBuilder result = services.AddOcelot();
            result.AddGatewayConsul();
            return result;
        }
    }
}
