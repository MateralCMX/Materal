using Materal.Gateway.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Ocelot.Configuration;
using Ocelot.Configuration.Repository;
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
    /// ��������·�ɲ����м��
    /// </summary>
    /// <remarks>
    /// ���췽��
    /// </remarks>
    public class GatewayDownstreamRouteFinderMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory, IDownstreamRouteProviderFactory downstreamRouteFinder) : OcelotMiddleware(loggerFactory.CreateLogger<DownstreamRouteFinderMiddleware>())
    {
        /// <summary>
        /// �м��ִ��
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            string upstreamUrlPath = httpContext.Request.Path.ToString();
            string upstreamQueryString = httpContext.Request.QueryString.ToString();
            StringValues upstreamHost = httpContext.Request.Headers["Host"];
            Logger.LogDebug($"����Url·��Ϊ:{upstreamUrlPath}");
            IInternalConfiguration internalConfiguration = httpContext.Items.IInternalConfiguration();
            IDownstreamRouteProvider provider = downstreamRouteFinder.Get(internalConfiguration);
            Response<DownstreamRouteHolder> response = provider.Get(upstreamUrlPath, upstreamQueryString, httpContext.Request.Method, internalConfiguration, upstreamHost);
            if (response.IsError)
            {
                Logger.LogDebug($"��������·�ɴ���:{response.Errors.ToErrorString()}");
                httpContext.Items.UpsertErrors(response.Errors);
                if (!GatewayConfig.IgnoreUnableToFindDownstreamRouteError) return;
                await next.Invoke(httpContext);
            }
            else
            {
                string downstreamPathTemplates = string.Join(", ", response.Data.Route.DownstreamRoute.Select(r => r.DownstreamPathTemplate.Value));
                Logger.LogDebug($"����ģ����:{downstreamPathTemplates}");
                httpContext.Items.UpsertTemplatePlaceholderNameAndValues(response.Data.TemplatePlaceholderNameAndValues);
                httpContext.Items.UpsertDownstreamRoute(response.Data);
                await next.Invoke(httpContext);
            }
        }
    }
}
