using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Ocelot.Configuration;
using Ocelot.DownstreamRouteFinder;
using Ocelot.DownstreamRouteFinder.Finder;
using Ocelot.DownstreamRouteFinder.Middleware;
using Ocelot.Infrastructure.Extensions;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Responses;

namespace Materal.Gateway.OcelotExtension.DownstreamRouteFinder.Middleware
{
    /// <summary>
    /// 网关下游路由查找中间件
    /// </summary>
    public class GatewayDownstreamRouteFinderMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDownstreamRouteProviderFactory _factory;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="downstreamRouteFinder"></param>
        public GatewayDownstreamRouteFinderMiddleware(RequestDelegate next,
            IOcelotLoggerFactory loggerFactory,
            IDownstreamRouteProviderFactory downstreamRouteFinder
            )
                : base(loggerFactory.CreateLogger<DownstreamRouteFinderMiddleware>())
        {
            _next = next;
            _factory = downstreamRouteFinder;
        }
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            string upstreamUrlPath = httpContext.Request.Path.ToString();
            string upstreamQueryString = httpContext.Request.QueryString.ToString();
            StringValues upstreamHost = httpContext.Request.Headers["Host"];
            Logger.LogDebug($"上游Url路径为:{upstreamUrlPath}");
            IInternalConfiguration internalConfiguration = httpContext.Items.IInternalConfiguration();
            IDownstreamRouteProvider provider = _factory.Get(internalConfiguration);
            Response<DownstreamRouteHolder> response = provider.Get(upstreamUrlPath, upstreamQueryString, httpContext.Request.Method, internalConfiguration, upstreamHost);
            if (response.IsError)
            {
                Logger.LogWarning($"{MiddlewareName}管道配置错误. 下游路由查找器返回:{response.Errors.ToErrorString()}");
                httpContext.Items.UpsertErrors(response.Errors);
                await _next.Invoke(httpContext);
            }
            else
            {
                string downstreamPathTemplates = string.Join(", ", response.Data.Route.DownstreamRoute.Select(r => r.DownstreamPathTemplate.Value));
                Logger.LogDebug($"下游模版是:{downstreamPathTemplates}");
                httpContext.Items.UpsertTemplatePlaceholderNameAndValues(response.Data.TemplatePlaceholderNameAndValues);
                httpContext.Items.UpsertDownstreamRoute(response.Data);
                await _next.Invoke(httpContext);
            }
        }
    }
}
