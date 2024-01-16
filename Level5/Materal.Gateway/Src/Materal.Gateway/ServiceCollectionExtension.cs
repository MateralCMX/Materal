using Materal.Gateway.OcelotExtension.Custom;
using Materal.Gateway.OcelotExtension.ExceptionInterceptor;
using Materal.Gateway.OcelotExtension.Repositories;
using Materal.Gateway.OcelotExtension.Requester;
using Materal.Gateway.OcelotExtension.RequestMonitor;
using Materal.Gateway.OcelotExtension.Responder;
using Materal.Gateway.OcelotExtension.ServiceDiscovery;
using Materal.Gateway.Service;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Ocelot.Requester;
using Ocelot.Responder;
using Ocelot.ServiceDiscovery;

namespace Materal.Gateway
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加Ocelot网关
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableConsul"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddOcelotGateway(this IServiceCollection services, bool enableConsul = true)
        {
            services.AddOptions();
            services.TryAddSingleton<IRequestMonitorHandlers, DefatultRequestMonitorHandlers>();
            services.TryAddSingleton<ICustomHandlers, DefaultCustomHandlers>();
            services.TryAddSingleton<IExceptionInterceptor, DefaultExceptionInterceptor>();
            services.TryAddSingleton<IOcelotConfigRepository, OcelotConfigRepositoryImpl>();
            services.TryAddSingleton<IOcelotConfigService, OcelotConfigServiceImpl>();
            services.TryAddSingleton<IServiceDiscoveryProviderFactory, GatewayServiceDiscoveryProviderFactory>();
            services.TryAddSingleton<IHttpRequester, GatewayHttpRequester>();
            services.TryAddSingleton<IHttpResponder, GatewayHttpContextResponder>();
            IOcelotBuilder result = services.AddOcelot();
            result.AddCacheManager(setting =>
            {
                setting.WithDictionaryHandle();
            });
            if (enableConsul)
            {
                result.AddConsul();
            }
            result.AddPolly();
            return result;
        }
    }
}
