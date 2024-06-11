using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
#if NET8_0_OR_GREATER
using Ocelot.Provider.Consul.Interfaces;
#endif

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
            .AddSingleton<IConsulClientFactory, ConsulClientFactory>()
#if NET8_0_OR_GREATER
            .AddSingleton(ConsulProviderFactory.GetConfiguration)
            .AddSingleton<IConsulServiceBuilder, DefaultConsulServiceBuilder>()
#endif
            .RemoveAll(typeof(IFileConfigurationPollerOptions))
            .AddSingleton<IFileConfigurationPollerOptions, ConsulFileConfigurationPollerOption>();
            return builder;
        }
    }
}
