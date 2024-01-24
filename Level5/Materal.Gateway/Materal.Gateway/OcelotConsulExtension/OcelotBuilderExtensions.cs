﻿using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;

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
                .AddSingleton(GatewayConsulProviderFactory.Get)
                .AddSingleton<IConsulClientFactory, ConsulClientFactory>()
                .RemoveAll(typeof(IFileConfigurationPollerOptions))
                .AddSingleton<IFileConfigurationPollerOptions, ConsulFileConfigurationPollerOption>();
            return builder;
        }
    }
}