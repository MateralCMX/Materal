using Ocelot.Configuration;
using Ocelot.DownstreamRouteFinder;
using Ocelot.DownstreamRouteFinder.Finder;
using Ocelot.Infrastructure.Extensions;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Responses;

namespace Materal.Gateway.DownstreamRouteFinder.Middleware
{
    /// <summary>
    /// 网关下游路由查找中间件
    /// </summary>
    public class GatewayDownstreamRouteFinderMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory, IDownstreamRouteProviderFactory downstreamRouteFinder) : OcelotMiddleware(loggerFactory.CreateLogger<GatewayDownstreamRouteFinderMiddleware>())
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            string upstreamUrlPath = httpContext.Request.Path.ToString();
            string upstreamQueryString = httpContext.Request.QueryString.ToString();
            IInternalConfiguration internalConfiguration = httpContext.Items.IInternalConfiguration();
            string hostHeader = httpContext.Request.Headers.Host.ToString();
            string upstreamHost = hostHeader.Contains(':')
                ? hostHeader.Split(':')[0]
                : hostHeader;
            Dictionary<string, string> upstreamHeaders = httpContext.Request.Headers
                .ToDictionary(h => h.Key, h => string.Join(';', h.Value.ToString()));
            Logger.LogDebug($"上游Url路径为:{upstreamUrlPath}");
            IDownstreamRouteProvider provider = downstreamRouteFinder.Get(internalConfiguration);
            Response<DownstreamRouteHolder> response = provider.Get(upstreamUrlPath, upstreamQueryString, httpContext.Request.Method, internalConfiguration, upstreamHost, upstreamHeaders);
            if (response.IsError)
            {
                Logger.LogDebug($"查找下游路由错误:{response.Errors.ToErrorString()}");
                httpContext.Items.UpsertErrors(response.Errors);
                await next.Invoke(httpContext);
            }
            else
            {
                string downstreamPathTemplates = string.Join(", ", response.Data.Route.DownstreamRoute.Select(r => r.DownstreamPathTemplate.Value));
                Logger.LogDebug($"下游模版是:{downstreamPathTemplates}");
                httpContext.Items.UpsertTemplatePlaceholderNameAndValues(response.Data.TemplatePlaceholderNameAndValues);
                httpContext.Items.UpsertDownstreamRoute(response.Data);
                await next.Invoke(httpContext);
            }
        }
    }
}
