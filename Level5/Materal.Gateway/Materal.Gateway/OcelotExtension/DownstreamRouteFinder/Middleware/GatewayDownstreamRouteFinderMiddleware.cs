using Materal.Gateway.Common;
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
    /// <remarks>
    /// 构造方法
    /// </remarks>
    public class GatewayDownstreamRouteFinderMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory, IDownstreamRouteProviderFactory downstreamRouteFinder) : OcelotMiddleware(loggerFactory.CreateLogger<DownstreamRouteFinderMiddleware>())
    {
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
            Dictionary<string, string> upstreamHeaders = httpContext.Request.Headers
                .ToDictionary(h => h.Key, h =>
                {
                    string value = h.Value.ToString();
                    return string.Join(';', value);
                });
            Logger.LogDebug($"上游Url路径为:{upstreamUrlPath}");
            IInternalConfiguration internalConfiguration = httpContext.Items.IInternalConfiguration();
            IDownstreamRouteProvider provider = downstreamRouteFinder.Get(internalConfiguration);
#if NET8_0_OR_GREATER
            Response<DownstreamRouteHolder> response = provider.Get(upstreamUrlPath, upstreamQueryString, httpContext.Request.Method, internalConfiguration, upstreamHost, upstreamHeaders);
#elif NET6_0
            Response<DownstreamRouteHolder> response = provider.Get(upstreamUrlPath, upstreamQueryString, httpContext.Request.Method, internalConfiguration, upstreamHost);
#endif
            if (response.IsError)
            {
                Logger.LogDebug($"查找下游路由错误:{response.Errors.ToErrorString()}");
                httpContext.Items.UpsertErrors(response.Errors);
                if (!GatewayConfig.IgnoreUnableToFindDownstreamRouteError) return;
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
