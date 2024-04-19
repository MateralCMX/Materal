#if NET6_0
using Ocelot.Configuration;
using Ocelot.Requester;
using IHttpClientBuilder = Ocelot.Requester.IHttpClientBuilder;

namespace Materal.Gateway.OcelotExtension.Requester
{
    /// <summary>
    /// 网关HTTP客户端构造器
    /// </summary>
    public class GatewayHttpClientBuilder(IDelegatingHandlerHandlerFactory factory, IHttpClientCache cacheHandlers) : IHttpClientBuilder
    {
        private DownstreamRoute? _cacheKey;
        private HttpClient? _httpClient;
        private IHttpClient? _client;
        private readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(90);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="downstreamRoute"></param>
        /// <returns></returns>
        public IHttpClient Create(DownstreamRoute downstreamRoute)
        {
            _cacheKey = downstreamRoute;
            var httpClient = cacheHandlers.Get(_cacheKey);
            if (httpClient is not null)
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
        /// <summary>
        /// 创建处理器
        /// </summary>
        /// <param name="downstreamRoute"></param>
        /// <returns></returns>
        private static HttpClientHandler CreateHandler(DownstreamRoute downstreamRoute)
        {
            var useCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer;
            return useCookies ? UseCookiesHandler(downstreamRoute) : UseNonCookiesHandler(downstreamRoute);
        }
        /// <summary>
        /// 使用非Cookie处理器
        /// </summary>
        /// <param name="downstreamRoute"></param>
        /// <returns></returns>
        private static HttpClientHandler UseNonCookiesHandler(DownstreamRoute downstreamRoute) => new()
        {
            AllowAutoRedirect = downstreamRoute.HttpHandlerOptions.AllowAutoRedirect,
            UseCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer,
            UseProxy = downstreamRoute.HttpHandlerOptions.UseProxy,
            MaxConnectionsPerServer = downstreamRoute.HttpHandlerOptions.MaxConnectionsPerServer,
        };
        /// <summary>
        /// 使用Cookie处理器
        /// </summary>
        /// <param name="downstreamRoute"></param>
        /// <returns></returns>
        private static HttpClientHandler UseCookiesHandler(DownstreamRoute downstreamRoute) => new()
        {
            AllowAutoRedirect = downstreamRoute.HttpHandlerOptions.AllowAutoRedirect,
            UseCookies = downstreamRoute.HttpHandlerOptions.UseCookieContainer,
            UseProxy = downstreamRoute.HttpHandlerOptions.UseProxy,
            MaxConnectionsPerServer = downstreamRoute.HttpHandlerOptions.MaxConnectionsPerServer,
            CookieContainer = new CookieContainer(),
        };
        /// <summary>
        /// 保存
        /// </summary>
        public void Save() => cacheHandlers.Set(_cacheKey, _client, TimeSpan.FromHours(24));
        /// <summary>
        /// 创建HTTP消息处理器
        /// </summary>
        /// <param name="httpMessageHandler"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private HttpMessageHandler CreateHttpMessageHandler(HttpMessageHandler httpMessageHandler, DownstreamRoute request)
        {
            var handlers = factory.Get(request).Data;
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
#endif