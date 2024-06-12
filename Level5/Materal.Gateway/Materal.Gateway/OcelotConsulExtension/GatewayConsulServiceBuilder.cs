using Consul;
using Ocelot.Infrastructure.Extensions;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;
using Ocelot.Values;

namespace Materal.Gateway.OcelotConsulExtension
{
    /// <summary>
    /// 网关Consul服务生成器
    /// </summary>
    public class GatewayConsulServiceBuilder : IConsulServiceBuilder
    {
        private const string VersionPrefix = "version-";
        private readonly ConsulRegistryConfiguration _configuration;
        /// <summary>
        /// 配置
        /// </summary>
        public ConsulRegistryConfiguration Configuration => _configuration;
        private readonly IConsulClient _client;
        private readonly IOcelotLogger _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        public GatewayConsulServiceBuilder(Func<ConsulRegistryConfiguration> configurationFactory, IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory)
        {
            _configuration = configurationFactory.Invoke();
            _client = clientFactory.Get(_configuration);
            _logger = loggerFactory.CreateLogger<DefaultConsulServiceBuilder>();
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public virtual bool IsValid(ServiceEntry entry)
        {
            AgentService service = entry.Service;
            string address = service.Address;
            bool valid = !string.IsNullOrEmpty(address)
                && !address.StartsWith(Uri.UriSchemeHttp + "://", StringComparison.OrdinalIgnoreCase)
                && !address.StartsWith(Uri.UriSchemeHttps + "://", StringComparison.OrdinalIgnoreCase)
                && service.Port > 0;
            if (!valid)
            {
                _logger.LogWarning(() => $"无法使用服务{service.Service}地址:{service.Address}:{service.Port}");
            }
            return valid;
        }
        /// <summary>
        /// 构建服务
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public virtual IEnumerable<Ocelot.Values.Service> BuildServices(ServiceEntry[] entries, Node[] nodes)
        {
            ArgumentNullException.ThrowIfNull(entries);
            List<Ocelot.Values.Service> services = new(entries.Length);
            foreach (var serviceEntry in entries)
            {
                if (!IsValid(serviceEntry)) continue;
                Node? serviceNode = GetNode(serviceEntry, nodes);
                Ocelot.Values.Service item = CreateService(serviceEntry, serviceNode);
                if (item is null) continue;
                services.Add(item);
            }
            return services;
        }
        /// <summary>
        /// 获得节点
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        protected virtual Node? GetNode(ServiceEntry entry, Node[] nodes) => entry?.Node ?? nodes?.FirstOrDefault(n => n.Address == entry?.Service?.Address);
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public virtual Ocelot.Values.Service CreateService(ServiceEntry entry, Node? node)
            => new(
                GetServiceName(entry, node),
                GetServiceHostAndPort(entry, node),
                GetServiceId(entry, node),
                GetServiceVersion(entry, node),
                GetServiceTags(entry, node)
            );
        /// <summary>
        /// 获得服务名称
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual string GetServiceName(ServiceEntry entry, Node? node)
            => entry.Service.Service;
        /// <summary>
        /// 获得服务主机和端口
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual ServiceHostAndPort GetServiceHostAndPort(ServiceEntry entry, Node? node)
            => new(
                GetDownstreamHost(entry, node),
                entry.Service.Port);
        /// <summary>
        /// 获得下游主机
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual string GetDownstreamHost(ServiceEntry entry, Node? node)
            => entry.Service.Address;
        /// <summary>
        /// 获得服务ID
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual string GetServiceId(ServiceEntry entry, Node node)
            => entry.Service.ID;
        /// <summary>
        /// 获得服务版本
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual string GetServiceVersion(ServiceEntry entry, Node node)
            => entry.Service.Tags
                ?.FirstOrDefault(tag => tag.StartsWith(VersionPrefix, StringComparison.Ordinal))
                ?.TrimStart(VersionPrefix)
                ?? string.Empty;
        /// <summary>
        /// 获得服务标签
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual IEnumerable<string> GetServiceTags(ServiceEntry entry, Node node)
            => entry.Service.Tags ?? Enumerable.Empty<string>();
    }
}
