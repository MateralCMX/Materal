using Materal.Gateway.OcelotExtension.Custom;
using Materal.Gateway.OcelotExtension.ExceptionInterceptor;
using Materal.Gateway.OcelotExtension.Repositories;
using Materal.Gateway.OcelotExtension.Requester;
using Materal.Gateway.OcelotExtension.RequestMonitor;
using Materal.Gateway.OcelotExtension.Responder;
using Materal.Gateway.OcelotExtension.ServiceDiscovery;
using Materal.Gateway.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
    public static class DIExtension
    {
        /// <summary>
        /// 添加Ocelot网关
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableConsul"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddOcelotGateway(this IServiceCollection services, bool enableConsul = true)
        {
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
            services.TryAddSingleton<IRequestMonitorHandlers, DefatultRequestMonitorHandlers>();
            services.TryAddSingleton<ICustomHandlers, DefaultCustomHandlers>();
            services.TryAddSingleton<IExceptionInterceptor, DefaultExceptionInterceptor>();
            services.TryAddSingleton<IOcelotConfigRepository, OcelotConfigRepositoryImpl>();
            services.TryAddSingleton<IOcelotConfigService, OcelotConfigServiceImpl>();
            services.Replace(new ServiceDescriptor(typeof(IServiceDiscoveryProviderFactory), typeof(GatewayServiceDiscoveryProviderFactory), ServiceLifetime.Singleton));
            services.Replace(new ServiceDescriptor(typeof(IHttpRequester), typeof(GatewayHttpRequester), ServiceLifetime.Singleton));
            services.Replace(new ServiceDescriptor(typeof(IHttpResponder), typeof(GatewayHttpContextResponder), ServiceLifetime.Singleton));
            return result;
        }
    }
}
