//using Consul;
//using Ocelot.Logging;
//using Ocelot.Provider.Consul;
//using Ocelot.Provider.Consul.Interfaces;
//using Ocelot.ServiceDiscovery.Providers;

//namespace Materal.Gateway.OcelotConsulExtension
//{
//    /// <summary>
//    /// 网关Consul
//    /// </summary>
//    public class GatewayConsul : IServiceDiscoveryProvider
//    {
//        private readonly ConsulRegistryConfiguration _configuration;
//        private readonly IConsulClient _consul;
//        private readonly IOcelotLogger _logger;
//        private readonly IConsulServiceBuilder _serviceBuilder;
//        /// <summary>
//        /// 构造方法
//        /// </summary>
//        public GatewayConsul(ConsulRegistryConfiguration config, IOcelotLoggerFactory factory, IConsulClientFactory clientFactory, IConsulServiceBuilder serviceBuilder)
//        {
//            _configuration = config;
//            _consul = clientFactory.Get(_configuration);
//            _logger = factory.CreateLogger<GatewayConsul>();
//            _serviceBuilder = serviceBuilder;
//        }
//        /// <summary>
//        /// 获取服务
//        /// </summary>
//        /// <returns></returns>
//        public virtual async Task<List<Ocelot.Values.Service>> GetAsync()
//        {
//            Task<QueryResult<ServiceEntry[]>> entriesTask = _consul.Health.Service(_configuration.KeyOfServiceInConsul, string.Empty, true);
//            Task<QueryResult<Node[]>> nodesTask = _consul.Catalog.Nodes();
//            await Task.WhenAll(entriesTask, nodesTask);
//            ServiceEntry[] entries = entriesTask.Result.Response ?? [];
//            Node[] nodes = nodesTask.Result.Response ?? [];
//            List<Ocelot.Values.Service> services = [];
//            if (entries.Length != 0)
//            {
//                _logger.LogDebug(() => $"在Consul中找到{entries.Length}个{_configuration.KeyOfServiceInConsul}服务");
//                _logger.LogDebug(() => $"在Consul中找到{nodes.Length}个节点.");
//                IEnumerable<Ocelot.Values.Service> collection = BuildServices(entries, nodes);
//                services.AddRange(collection);
//            }
//            else
//            {
//                _logger.LogWarning(() => $"没有在Consul中找到'{_configuration.KeyOfServiceInConsul}'服务!");
//            }
//            return services;
//        }
//        /// <summary>
//        /// 构建服务
//        /// </summary>
//        /// <param name="entries"></param>
//        /// <param name="nodes"></param>
//        /// <returns></returns>
//        protected virtual IEnumerable<Ocelot.Values.Service> BuildServices(ServiceEntry[] entries, Node[] nodes)
//            => _serviceBuilder.BuildServices(entries, nodes);
//        //private const string VersionPrefix = "version-";
//        //private readonly ConsulRegistryConfiguration _config;
//        //private readonly IConsulClient _consul;
//        //private readonly IOcelotLogger _logger;
//        ///// <summary>
//        ///// 构造方法
//        ///// </summary>
//        ///// <param name="config"></param>
//        ///// <param name="factory"></param>
//        ///// <param name="clientFactory"></param>
//        //public GatewayConsul(ConsulRegistryConfiguration config, IOcelotLoggerFactory factory, IConsulClientFactory clientFactory, IConsulServiceBuilder? serviceBuilder)
//        //{
//        //    _config = config;
//        //    _consul = clientFactory.Get(_config);
//        //    _logger = factory.CreateLogger<GatewayConsul>();
//        //}
//        ///// <summary>
//        ///// 获取服务
//        ///// </summary>
//        ///// <returns></returns>
//        //public async Task<List<Ocelot.Values.Service>> GetAsync()
//        //{
//        //    QueryResult<ServiceEntry[]> queryResult = await _consul.Health.Service(_config.KeyOfServiceInConsul, string.Empty, true);
//        //    List<Ocelot.Values.Service> services = [];
//        //    foreach (ServiceEntry serviceEntry in queryResult.Response)
//        //    {
//        //        AgentService service = serviceEntry.Service;
//        //        if (IsValid(service))
//        //        {
//        //            QueryResult<Node[]> nodes = await _consul.Catalog.Nodes();
//        //            if (nodes.Response == null)
//        //            {
//        //                services.Add(BuildService(serviceEntry, null));
//        //            }
//        //            else
//        //            {
//        //                Node? serviceNode = nodes.Response.FirstOrDefault(n => n.Address == service.Address);
//        //                services.Add(BuildService(serviceEntry, serviceNode));
//        //            }
//        //        }
//        //        else
//        //        {
//        //            _logger.LogWarning($"Unable to use service address: '{service.Address}' and port: {service.Port} as it is invalid for the service: '{service.Service}'. Address must contain host only e.g. 'localhost', and port must be greater than 0.");
//        //        }
//        //    }
//        //    return [.. services];
//        //}
//        //private static Ocelot.Values.Service BuildService(ServiceEntry serviceEntry, Node? serviceNode)
//        //{
//        //    AgentService service = serviceEntry.Service;
//        //    string host = serviceNode is null ? service.Address : serviceNode.Address;
//        //    ServiceHostAndPort serviceHostAndPort = new(host, service.Port);
//        //    return new Ocelot.Values.Service(
//        //        service.Service,
//        //        serviceHostAndPort,
//        //        service.ID,
//        //        GetVersionFromStrings(service.Tags),
//        //        service.Tags ?? Enumerable.Empty<string>());
//        //}
//        //private static bool IsValid(AgentService service)
//        //    => !string.IsNullOrEmpty(service.Address)
//        //    && !service.Address.Contains($"{Uri.UriSchemeHttp}://")
//        //    && !service.Address.Contains($"{Uri.UriSchemeHttps}://")
//        //    && service.Port > 0;
//        //private static string? GetVersionFromStrings(IEnumerable<string> strings) => strings?.FirstOrDefault(x => x.StartsWith(VersionPrefix, StringComparison.Ordinal)).TrimStart(VersionPrefix);
//    }
//}
