using Consul;
using Ocelot.Infrastructure.Extensions;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;
#if NET8_0_OR_GREATER
using Ocelot.Provider.Consul.Interfaces;
#endif

namespace Materal.Gateway.OcelotConsulExtension
{
    /// <summary>
    /// 网关Consul
    /// </summary>
    public class GatewayConsul : IServiceDiscoveryProvider
    {
        private const string VersionPrefix = "version-";
        private readonly ConsulRegistryConfiguration _config;
        private readonly IConsulClient _consul;
        private readonly IOcelotLogger _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="config"></param>
        /// <param name="factory"></param>
        /// <param name="clientFactory"></param>
        public GatewayConsul(ConsulRegistryConfiguration config, IOcelotLoggerFactory factory, IConsulClientFactory clientFactory)
        {
            _config = config;
            _consul = clientFactory.Get(_config);
            _logger = factory.CreateLogger<GatewayConsul>();
        }
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
#if NET8_0
        public async Task<List<Ocelot.Values.Service>> GetAsync()
#else
        public async Task<List<Ocelot.Values.Service>> Get()
#endif
        {
            QueryResult<ServiceEntry[]> queryResult = await _consul.Health.Service(_config.KeyOfServiceInConsul, string.Empty, true);
            List<Ocelot.Values.Service> services = [];
            foreach (ServiceEntry serviceEntry in queryResult.Response)
            {
                AgentService service = serviceEntry.Service;
                if (IsValid(service))
                {
                    QueryResult<Node[]> nodes = await _consul.Catalog.Nodes();
                    if (nodes.Response == null)
                    {
                        services.Add(BuildService(serviceEntry, null));
                    }
                    else
                    {
                        Node? serviceNode = nodes.Response.FirstOrDefault(n => n.Address == service.Address);
                        services.Add(BuildService(serviceEntry, serviceNode));
                    }
                }
                else
                {
                    _logger.LogWarning($"Unable to use service address: '{service.Address}' and port: {service.Port} as it is invalid for the service: '{service.Service}'. Address must contain host only e.g. 'localhost', and port must be greater than 0.");
                }
            }
            return [.. services];
        }
        private static Ocelot.Values.Service BuildService(ServiceEntry serviceEntry, Node? serviceNode)
        {
            AgentService service = serviceEntry.Service;
            string host = serviceNode is null ? service.Address : serviceNode.Address;
            ServiceHostAndPort serviceHostAndPort = new(host, service.Port);
            return new Ocelot.Values.Service(
                service.Service,
                serviceHostAndPort,
                service.ID,
                GetVersionFromStrings(service.Tags),
                service.Tags ?? Enumerable.Empty<string>());
        }
        private static bool IsValid(AgentService service)
            => !string.IsNullOrEmpty(service.Address)
            && !service.Address.Contains($"{Uri.UriSchemeHttp}://")
            && !service.Address.Contains($"{Uri.UriSchemeHttps}://")
            && service.Port > 0;
        private static string? GetVersionFromStrings(IEnumerable<string> strings) => strings?.FirstOrDefault(x => x.StartsWith(VersionPrefix, StringComparison.Ordinal)).TrimStart(VersionPrefix);
    }
}
