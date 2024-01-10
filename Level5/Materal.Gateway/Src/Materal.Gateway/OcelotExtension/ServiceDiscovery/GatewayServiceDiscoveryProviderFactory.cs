using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension.Middleware;
using Ocelot.Configuration;
using Ocelot.Responses;
using Ocelot.ServiceDiscovery;
using Ocelot.ServiceDiscovery.Configuration;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;

namespace Materal.Gateway.OcelotExtension.ServiceDiscovery
{
    /// <summary>
    /// ���ط������ṩ�߹���
    /// </summary>
    public class GatewayServiceDiscoveryProviderFactory : IServiceDiscoveryProviderFactory
    {
        private readonly ServiceDiscoveryFinderDelegate? _delegates;
        private readonly IServiceProvider _provider;
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <exception cref="GatewayException"></exception>
        public GatewayServiceDiscoveryProviderFactory()
        {
            if (OcelotService.Service == null) throw new GatewayException("���񲻴���");
            _provider = OcelotService.Service;
            _delegates = OcelotService.GetServiceOrDefault<ServiceDiscoveryFinderDelegate>();
        }
        /// <summary>
        /// ��ȡ�������ṩ��
        /// </summary>
        /// <param name="serviceConfig"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public Response<IServiceDiscoveryProvider> Get(ServiceProviderConfiguration serviceConfig, DownstreamRoute route)
        {
            if (route.UseServiceDiscovery)
            {
                return GetServiceDiscoveryProvider(serviceConfig, route);
            }
            List<Ocelot.Values.Service> services = new();
            foreach (var downstreamAddress in route.DownstreamAddresses)
            {
                Ocelot.Values.Service service = new(route.ServiceName, new ServiceHostAndPort(downstreamAddress.Host, downstreamAddress.Port, route.GetDownstreamScheme()), string.Empty, string.Empty, Array.Empty<string>());
                services.Add(service);
            }
            return new OkResponse<IServiceDiscoveryProvider>(new ConfigurationServiceProvider(services));
        }
        /// <summary>
        /// ��ȡ�������ṩ��
        /// </summary>
        /// <param name="config"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        private Response<IServiceDiscoveryProvider> GetServiceDiscoveryProvider(ServiceProviderConfiguration config, DownstreamRoute route)
        {
            if (config.Type?.ToLower() == "servicefabric")
            {
                var sfConfig = new ServiceFabricConfiguration(config.Host, config.Port, route.ServiceName);
                return new OkResponse<IServiceDiscoveryProvider>(new ServiceFabricServiceDiscoveryProvider(sfConfig));
            }
            if (_delegates != null)
            {
                IServiceDiscoveryProvider? provider = _delegates?.Invoke(_provider, config, route);
                if (provider == null || config.Type == null) throw new GatewayException("���ش���");
                if (provider.GetType().Name.ToLower() == config.Type.ToLower())
                {
                    return new OkResponse<IServiceDiscoveryProvider>(provider);
                }
            }
            return new ErrorResponse<IServiceDiscoveryProvider>(new UnableToFindServiceDiscoveryProviderError($"Unable to find service discovery provider for type: {config.Type}"));
        }
    }
}
