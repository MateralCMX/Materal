using Materal.Gateway.OcelotExtension.Custom;
using Materal.Gateway.OcelotExtension.ExceptionInterceptor;
using Materal.Gateway.OcelotExtension.Requester;
using Materal.Gateway.OcelotExtension.RequestMonitor;
using Materal.Gateway.OcelotExtension.Responder;
using Materal.Gateway.OcelotExtension.ServiceDiscovery;
using Materal.Gateway.OcelotExtension.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Ocelot.Requester;
using Ocelot.Responder;
using Ocelot.ServiceDiscovery;

namespace Materal.Gateway.OcelotExtension
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
        /// <returns></returns>
        public static IOcelotBuilder AddOcelotGatewayAsync(this IServiceCollection services)
        {
            IOcelotBuilder result = services.AddOcelot();
            result.AddCacheManager(setting =>
            {
                setting.WithDictionaryHandle();
            });
            result.AddConsul();
            result.AddPolly();
            services.TryAddSingleton<IRequestMonitorHandlers, DefatultRequestMonitorHandlers>();
            services.TryAddSingleton<ICustomHandlers, DefaultCustomHandlers>();
            services.TryAddSingleton<IExceptionInterceptor, DefaultExceptionInterceptor>();
            services.AddSingleton<IOcelotConfigService, OcelotConfigService>();
            services.Replace(new ServiceDescriptor(typeof(IServiceDiscoveryProviderFactory), typeof(GatewayServiceDiscoveryProviderFactory), ServiceLifetime.Singleton));
            services.Replace(new ServiceDescriptor(typeof(IHttpRequester), typeof(GatewayHttpRequester), ServiceLifetime.Singleton));
            services.Replace(new ServiceDescriptor(typeof(IHttpResponder), typeof(GatewayHttpContextResponder), ServiceLifetime.Singleton));
            return result;
        }
    }
}
