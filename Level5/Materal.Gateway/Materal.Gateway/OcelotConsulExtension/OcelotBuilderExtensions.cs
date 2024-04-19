using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.ServiceDiscovery;

namespace Materal.Gateway.OcelotConsulExtension
{
    /// <summary>
    /// OcelotBuilder扩展
    /// </summary>
    public static class OcelotBuilderExtensions
    {
        /// <summary>
        /// 添加网关Consul
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddGatewayConsul(this IOcelotBuilder builder)
        {
            builder.AddConsul();
            builder.Services.Replace(new ServiceDescriptor(typeof(ServiceDiscoveryFinderDelegate), GatewayConsulProviderFactory.Get));
            return builder;
        }
    }
}
