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
    public class GatewayServiceDiscoveryProviderFactory : IServiceDiscoveryProviderFactory
    {
        private readonly ServiceDiscoveryFinderDelegate? _delegates;
        private readonly IServiceProvider _provider;
        public GatewayServiceDiscoveryProviderFactory()
        {
            if (OcelotService.Service == null) throw new GatewayException("服务不存在");
            _provider = OcelotService.Service;
            _delegates = OcelotService.GetServiceOrDefault<ServiceDiscoveryFinderDelegate>();
        }
        public Response<IServiceDiscoveryProvider> Get(ServiceProviderConfiguration serviceConfig, DownstreamRoute route)
        {
            if (route.UseServiceDiscovery)
            {
                return GetServiceDiscoveryProvider(serviceConfig, route);
            }
            List<Service> services = new();
            foreach (var downstreamAddress in route.DownstreamAddresses)
            {
                Service service = new(route.ServiceName, new ServiceHostAndPort(downstreamAddress.Host, downstreamAddress.Port, route.GetDownstreamScheme()), string.Empty, string.Empty, Array.Empty<string>());
                services.Add(service);
            }
            return new OkResponse<IServiceDiscoveryProvider>(new ConfigurationServiceProvider(services));
        }
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
                if (provider == null || config.Type == null) throw new GatewayException("网关错误");
                if (provider.GetType().Name.ToLower() == config.Type.ToLower())
                {
                    return new OkResponse<IServiceDiscoveryProvider>(provider);
                }
            }
            return new ErrorResponse<IServiceDiscoveryProvider>(new UnableToFindServiceDiscoveryProviderError($"Unable to find service discovery provider for type: {config.Type}"));
        }
    }
}
