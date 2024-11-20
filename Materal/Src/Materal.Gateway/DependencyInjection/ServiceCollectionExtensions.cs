using Materal.Gateway.Provider.Consul;
using Materal.Gateway.Repositories;
using Materal.Gateway.Service;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace Materal.Gateway.DependencyInjection
{
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加网关
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddGateway(this IServiceCollection services)
        {
            services.TryAddSingleton<IOcelotConfigRepository, OcelotConfigRepositoryImpl>();
            services.TryAddSingleton<IOcelotConfigService, OcelotConfigServiceImpl>();
            IOcelotBuilder ocelotBuilder = services.AddOcelot();
            ocelotBuilder.AddCacheManager(setting =>
            {
                setting.WithDictionaryHandle();
            });
            ocelotBuilder.AddConsul<GatewayConsulServiceBuilder>();
            ocelotBuilder.AddPolly();
            return services;
        }
    }
}
