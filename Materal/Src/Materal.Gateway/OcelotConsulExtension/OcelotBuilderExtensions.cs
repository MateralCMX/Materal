using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Consul.Interfaces;

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
            // 原来的是Ocelot.Provider.Consul.DefaultConsulServiceBuilder
            builder.Services.Replace(new ServiceDescriptor(typeof(IConsulServiceBuilder), typeof(GatewayConsulServiceBuilder), ServiceLifetime.Singleton));
            return builder;
        }
    }
}
