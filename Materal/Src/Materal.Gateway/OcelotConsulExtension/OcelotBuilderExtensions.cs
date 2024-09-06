using Ocelot.Configuration.Repository;
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
            builder.Services
                .AddSingleton(ConsulProviderFactory.Get)
                .AddSingleton(ConsulProviderFactory.GetConfiguration)
                .AddSingleton<IConsulClientFactory, ConsulClientFactory>()
                .AddSingleton<IConsulServiceBuilder, GatewayConsulServiceBuilder>()
                .RemoveAll(typeof(IFileConfigurationPollerOptions))
                .AddSingleton<IFileConfigurationPollerOptions, ConsulFileConfigurationPollerOption>();
            return builder;
        }
    }
}
