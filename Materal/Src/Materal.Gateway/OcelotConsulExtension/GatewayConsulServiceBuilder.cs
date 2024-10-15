using Consul;
using Ocelot.Logging;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;

namespace Materal.Gateway.OcelotConsulExtension
{
    /// <summary>
    /// 网关Consul服务构建器
    /// </summary>
    /// <param name="contextAccessor"></param>
    /// <param name="clientFactory"></param>
    /// <param name="loggerFactory"></param>
    public class GatewayConsulServiceBuilder(IHttpContextAccessor contextAccessor, IConsulClientFactory clientFactory, IOcelotLoggerFactory loggerFactory) : DefaultConsulServiceBuilder(contextAccessor, clientFactory, loggerFactory), IConsulServiceBuilder
    {
        /// <inheritdoc/>
        protected override string GetDownstreamHost(ServiceEntry entry, Node node)
        {
            //原来的方法会导致获取不到下游服务地址
            //return node != null ? node.Name : entry.Service.Address;
            return entry.Service.Address;
        }
    }
}
