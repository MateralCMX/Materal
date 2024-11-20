using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;

namespace Materal.Gateway.Provider.Consul
{
    /// <summary>
    /// 网关Consul服务生成器
    /// </summary>
    /// <param name="contextAccessor"></param>
    /// <param name="clientFactory"></param>
    /// <param name="loggerFactory"></param>
    public partial class GatewayConsulServiceBuilder(IHttpContextAccessor contextAccessor, IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory) : DefaultConsulServiceBuilder(contextAccessor, clientFactory, loggerFactory)
    {
        /// <inheritdoc/>
        protected override string GetDownstreamHost(ServiceEntry entry, Node node)
            => entry.Service.Address;
    }
}
