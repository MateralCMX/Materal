//using Ocelot.Configuration;
//using Ocelot.Logging;
//using Ocelot.Provider.Consul;
//using Ocelot.Provider.Consul.Interfaces;
//using Ocelot.ServiceDiscovery;
//using Ocelot.ServiceDiscovery.Providers;

//namespace Materal.Gateway.OcelotConsulExtension
//{
//    /// <summary>
//    /// 网关Consul提供者工厂
//    /// </summary>
//    public static class GatewayConsulProviderFactory
//    {
//        /// <summary>
//        /// String constant used for provider type definition.
//        /// </summary>
//        public const string PollConsul = nameof(Ocelot.Provider.Consul.PollConsul);
//        private static readonly List<PollConsul> ServiceDiscoveryProviders = [];
//        private static readonly object LockObject = new();
//        /// <summary>
//        /// 获取服务发现提供者
//        /// </summary>
//        public static ServiceDiscoveryFinderDelegate Get { get; } = CreateProvider;
//        /// <summary>
//        /// 配置
//        /// </summary>
//        private static ConsulRegistryConfiguration? configuration;
//        private static ConsulRegistryConfiguration? ConfigurationGetter() => configuration;
//        /// <summary>
//        /// 获取配置
//        /// </summary>
//        public static Func<ConsulRegistryConfiguration?> GetConfiguration { get; } = ConfigurationGetter;
//        private static IServiceDiscoveryProvider CreateProvider(IServiceProvider provider, ServiceProviderConfiguration config, DownstreamRoute route)
//        {
//            IOcelotLoggerFactory factory = provider.GetRequiredService<IOcelotLoggerFactory>();
//            IConsulClientFactory consulFactory = provider.GetRequiredService<IConsulClientFactory>();
//            configuration = new ConsulRegistryConfiguration(config.Scheme, config.Host, config.Port, route.ServiceName, config.Token);
//            IConsulServiceBuilder? serviceBuilder = provider.GetService<IConsulServiceBuilder>();
//            GatewayConsul consulProvider = new(configuration, factory, consulFactory, serviceBuilder);
//            if (PollConsul.Equals(config.Type, StringComparison.OrdinalIgnoreCase))
//            {
//                lock (LockObject)
//                {
//                    var discoveryProvider = ServiceDiscoveryProviders.FirstOrDefault(x => x.ServiceName == route.ServiceName);
//                    if (discoveryProvider != null)
//                    {
//                        return discoveryProvider;
//                    }

//                    discoveryProvider = new PollConsul(config.PollingInterval, route.ServiceName, factory, consulProvider);
//                    ServiceDiscoveryProviders.Add(discoveryProvider);
//                    return discoveryProvider;
//                }
//            }

//            return consulProvider;
//        }
//    }
//}
