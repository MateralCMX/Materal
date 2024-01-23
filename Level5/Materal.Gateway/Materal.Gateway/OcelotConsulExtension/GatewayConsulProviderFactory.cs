using Consul;
using Ocelot.Configuration;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.ServiceDiscovery;
using Ocelot.ServiceDiscovery.Providers;

namespace Materal.Gateway.OcelotConsulExtension
{
    /// <summary>
    /// 网关Consul提供者工厂
    /// </summary>
    public static class GatewayConsulProviderFactory
    {
        /// <summary>
        /// String constant used for provider type definition.
        /// </summary>
        public const string PollConsul = nameof(Ocelot.Provider.Consul.PollConsul);
        private static readonly List<PollConsul> ServiceDiscoveryProviders = [];
        private static readonly object LockObject = new();
        /// <summary>
        /// Gets the provider.
        /// </summary>
        public static ServiceDiscoveryFinderDelegate Get { get; } = CreateProvider;

        private static IServiceDiscoveryProvider CreateProvider(IServiceProvider provider,
            ServiceProviderConfiguration config, DownstreamRoute route)
        {
            IOcelotLoggerFactory? factory = provider.GetRequiredService<IOcelotLoggerFactory>();
            IConsulClientFactory? consulFactory = provider.GetRequiredService<IConsulClientFactory>();
            ConsulRegistryConfiguration consulRegistryConfiguration = new(config.Scheme, config.Host, config.Port, route.ServiceName, config.Token);
            GatewayConsul consulProvider = new(consulRegistryConfiguration, factory, consulFactory);
            if (PollConsul.Equals(config.Type, StringComparison.OrdinalIgnoreCase))
            {
                lock (LockObject)
                {
                    var discoveryProvider = ServiceDiscoveryProviders.FirstOrDefault(x => x.ServiceName == route.ServiceName);
                    if (discoveryProvider != null)
                    {
                        return discoveryProvider;
                    }

                    discoveryProvider = new PollConsul(config.PollingInterval, route.ServiceName, factory, consulProvider);
                    ServiceDiscoveryProviders.Add(discoveryProvider);
                    return discoveryProvider;
                }
            }
            return consulProvider;
        }
    }
}
