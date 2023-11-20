using Kros.Extensions;
using Microsoft.Extensions.Options;
using MMLib.SwaggerForOcelot.Configuration;
using MMLib.SwaggerForOcelot.ServiceDiscovery;
using Ocelot.Configuration.Builder;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.File;
using Ocelot.Responses;
using Ocelot.ServiceDiscovery;
using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;
using Swashbuckle.AspNetCore.Swagger;
using RouteOptions = MMLib.SwaggerForOcelot.Configuration.RouteOptions;

namespace Materal.Gateway.WebAPP
{
    /// <summary>
    /// In your project, create a new class and implement the interface ISwaggerServiceDiscoveryProvider.
    /// </summary>
    public class MySwaggerServiceDiscoveryProvider : ISwaggerServiceDiscoveryProvider
    {
        private readonly IServiceDiscoveryProviderFactory _serviceDiscovery;
        private readonly IServiceProviderConfigurationCreator _configurationCreator;
        private readonly IOptionsMonitor<FileConfiguration> _options;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<SwaggerOptions> _swaggerOptions;
        public MySwaggerServiceDiscoveryProvider(
            IServiceDiscoveryProviderFactory serviceDiscovery,
            IServiceProviderConfigurationCreator configurationCreator,
            IOptionsMonitor<FileConfiguration> options,
            IHttpContextAccessor httpContextAccessor,
            IOptions<SwaggerOptions> swaggerOptions)
        {
            _serviceDiscovery = serviceDiscovery;
            _configurationCreator = configurationCreator;
            _options = options;
            _httpContextAccessor = httpContextAccessor;
            _swaggerOptions = swaggerOptions;
        }

        /// <inheritdoc />
        public async Task<Uri> GetSwaggerUriAsync(SwaggerEndPointConfig endPoint, RouteOptions route)
        {
            if (endPoint.Version == "aggregates" || endPoint.Version == "gateway")
            {
                return GetGatewayItSelfSwaggerPath(endPoint);
            }
            else if (!endPoint.Url.IsNullOrEmpty())
            {
                return new Uri(endPoint.Url);
            }
            else
            {
                return await GetSwaggerUri(endPoint, route);
            }
        }

        private Uri GetGatewayItSelfSwaggerPath(SwaggerEndPointConfig endPoint)
        {
            if(_httpContextAccessor.HttpContext is null) throw new InvalidOperationException(GetErrorMessage(endPoint));
            var builder = new UriBuilder(
                _httpContextAccessor.HttpContext.Request.Scheme,
                _httpContextAccessor.HttpContext.Request.Host.Host)
            {
                Path = _swaggerOptions.Value
                    .RouteTemplate.Replace("{documentName}", endPoint.Version).Replace("{json|yaml}", "json")
            };

            if (_httpContextAccessor.HttpContext.Request.Host.Port.HasValue && _httpContextAccessor.HttpContext.Request.Host.Port is not null)
            {
                builder.Port = _httpContextAccessor.HttpContext.Request.Host.Port.Value;
            }

            return builder.Uri;
        }

        private async Task<Uri> GetSwaggerUri(SwaggerEndPointConfig endPoint, RouteOptions route)
        {
            var conf = _configurationCreator.Create(_options.CurrentValue.GlobalConfiguration);

            var downstreamRoute = new DownstreamRouteBuilder()
                .WithUseServiceDiscovery(true)
                .WithServiceName(endPoint.Service.Name)
                .WithServiceNamespace(route?.ServiceNamespace)
                .Build();

            Response<IServiceDiscoveryProvider> serviceProvider = _serviceDiscovery.Get(conf, downstreamRoute);

            if (serviceProvider.IsError)
            {
                throw new InvalidOperationException(GetErrorMessage(endPoint));
            }

            ServiceHostAndPort? service = ((await serviceProvider.Data.GetAsync()).FirstOrDefault()?.HostAndPort) ?? throw new InvalidOperationException(GetErrorMessage(endPoint));
            var builder = new UriBuilder(GetScheme(service, route), service.DownstreamHost, service.DownstreamPort)
            {
                Path = endPoint.Service.Path
            };
            return builder.Uri;
        }

        private string GetScheme(ServiceHostAndPort service, RouteOptions route)
            => (route != null && !route.DownstreamScheme.IsNullOrEmpty())
            ? route.DownstreamScheme
            : !service.Scheme.IsNullOrEmpty()
            ? service.Scheme
            : service.DownstreamPort
            switch
            {
                443 => Uri.UriSchemeHttps,
                80 => Uri.UriSchemeHttp,
                _ => string.Empty,
            };

        private static string GetErrorMessage(SwaggerEndPointConfig endPoint) => $"Service with swagger documentation '{endPoint.Service.Name}' cann't be discovered";
    }
}
