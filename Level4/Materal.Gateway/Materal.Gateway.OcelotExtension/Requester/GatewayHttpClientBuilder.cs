using Ocelot.Configuration;
using Ocelot.Requester;
using System.Net;

namespace Materal.Gateway.OcelotExtension.Requester
{
    public class GatewayHttpClientBuilder : IHttpClientBuilder
    {
        private readonly IDelegatingHandlerHandlerFactory _factory;
        private readonly IHttpClientCache _cacheHandlers;
        private DownstreamRoute? _cacheKey;
        private HttpClient? _httpClient;
        private IHttpClient? _client;
        private readonly TimeSpan _defaultTimeout;

        public GatewayHttpClientBuilder(IDelegatingHandlerHandlerFactory factory, IHttpClientCache cacheHandlers)
        {
            _factory = factory;
            _cacheHandlers = cacheHandlers;
            _defaultTimeout = TimeSpan.FromSeconds(90);
        }
        public IHttpClient Create(DownstreamRoute downstreamRoute)
        {
            _cacheKey = downstreamRoute;
            var httpClient = _cacheHandlers.Get(_cacheKey);
            if (httpClient != null)
            {
                _client = httpClient;
                return httpClient;
            }
            var handler = CreateHandler(downstreamRoute);
            if (downstreamRoute.DangerousAcceptAnyServerCertificateValidator)
            {
                handler.ServerCertificateCustomValidationCallback = (request, certificate, chain, errors) => true;
            }
            var timeout = downstreamRoute.QosOptions.TimeoutValue == 0
                ? _defaultTimeout
                : TimeSpan.FromMilliseconds(downstreamRoute.QosOptions.TimeoutValue);
            _httpClient = new HttpClient(CreateHttpMessageHandler(handler, downstreamRoute))
            {
                Timeout = timeout
            };
            _client = new HttpClientWrapper(_httpClient);
            return _client;
        }
        private HttpClientHandler CreateHandler(DownstreamRoute downstreamRoute)
        {
            var useCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer;
            return useCookies ? UseCookiesHandler(downstreamRoute) : UseNonCookiesHandler(downstreamRoute);
        }
        private HttpClientHandler UseNonCookiesHandler(DownstreamRoute downstreamRoute) => new HttpClientHandler
        {
            AllowAutoRedirect = downstreamRoute.HttpHandlerOptions.AllowAutoRedirect,
            UseCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer,
            UseProxy = downstreamRoute.HttpHandlerOptions.UseProxy,
            MaxConnectionsPerServer = downstreamRoute.HttpHandlerOptions.MaxConnectionsPerServer,
        };
        private HttpClientHandler UseCookiesHandler(DownstreamRoute downstreamRoute) => new HttpClientHandler
        {
            AllowAutoRedirect = downstreamRoute.HttpHandlerOptions.AllowAutoRedirect,
            UseCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer,
            UseProxy = downstreamRoute.HttpHandlerOptions.UseProxy,
            MaxConnectionsPerServer = downstreamRoute.HttpHandlerOptions.MaxConnectionsPerServer,
            CookieContainer = new CookieContainer(),
        };
        public void Save() => _cacheHandlers.Set(_cacheKey, _client, TimeSpan.FromHours(24));
        private HttpMessageHandler CreateHttpMessageHandler(HttpMessageHandler httpMessageHandler, DownstreamRoute request)
        {
            var handlers = _factory.Get(request).Data;
            handlers
                .Select(handler => handler)
                .Reverse()
                .ToList()
                .ForEach(handler =>
                {
                    var delegatingHandler = handler();
                    delegatingHandler.InnerHandler = httpMessageHandler;
                    httpMessageHandler = delegatingHandler;
                });
            return httpMessageHandler;
        }
    }
}
