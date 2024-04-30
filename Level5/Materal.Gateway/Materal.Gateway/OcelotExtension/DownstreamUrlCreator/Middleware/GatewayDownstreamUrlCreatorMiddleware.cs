using Materal.Gateway.OcelotExtension.Middleware;
using Ocelot.Configuration;
using Ocelot.DownstreamRouteFinder.UrlMatcher;
#if NET8_0
using Ocelot.DownstreamUrlCreator;
#else
using Ocelot.DownstreamUrlCreator.UrlTemplateReplacer;
#endif
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;
using Ocelot.Responses;
using Ocelot.Values;
using System.Text.RegularExpressions;

namespace Materal.Gateway.OcelotExtension.DownstreamUrlCreator.Middleware
{
    /// <summary>
    /// 网关下游URL创建中间件
    /// </summary>
    public class GatewayDownstreamUrlCreatorMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory, IDownstreamPathPlaceholderReplacer replacer) : OcelotMiddleware(loggerFactory.CreateLogger<GatewayDownstreamUrlCreatorMiddleware>())
    {
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            DownstreamRoute downstreamRoute = httpContext.Items.DownstreamRoute();
            List<PlaceholderNameAndValue> templatePlaceholderNameAndValues = httpContext.Items.TemplatePlaceholderNameAndValues();
            Response<DownstreamPath> response = replacer.Replace(downstreamRoute.DownstreamPathTemplate.Value, templatePlaceholderNameAndValues);
            DownstreamRequest downstreamRequest = httpContext.Items.DownstreamRequest();
            if (response.IsError)
            {
                httpContext.Items.UpsertErrors(response.Errors);
                return;
            }
            if (!string.IsNullOrEmpty(downstreamRoute.DownstreamScheme))
            {
                //todo make sure this works, hopefully there is a test ;E
                httpContext.Items.DownstreamRequest().Scheme = downstreamRoute.GetDownstreamScheme();
            }
            IInternalConfiguration internalConfiguration = httpContext.Items.IInternalConfiguration();
            if (ServiceFabricRequest(internalConfiguration, downstreamRoute))
            {
                (string path, string query) = CreateServiceFabricUri(downstreamRequest, downstreamRoute, templatePlaceholderNameAndValues, response);
                //todo check this works again hope there is a test..
                downstreamRequest.AbsolutePath = path;
                downstreamRequest.Query = query;
            }
            else
            {
                DownstreamPath dsPath = response.Data;
                if (ContainsQueryString(dsPath))
                {
                    downstreamRequest.AbsolutePath = GetPath(dsPath);
                    if (string.IsNullOrEmpty(downstreamRequest.Query))
                    {
                        downstreamRequest.Query = GetQueryString(dsPath);
                    }
                    else
                    {
                        downstreamRequest.Query += GetQueryString(dsPath).Replace('?', '&');
                    }
                }
                else
                {
                    RemoveQueryStringParametersThatHaveBeenUsedInTemplate(downstreamRequest, templatePlaceholderNameAndValues);
                    downstreamRequest.AbsolutePath = dsPath.Value;
                }
            }
            await next.Invoke(httpContext);
        }
        /// <summary>
        /// 移除已经在模板中使用的查询字符串参数
        /// </summary>
        /// <param name="downstreamRequest"></param>
        /// <param name="templatePlaceholderNameAndValues"></param>
        private static void RemoveQueryStringParametersThatHaveBeenUsedInTemplate(DownstreamRequest downstreamRequest, List<PlaceholderNameAndValue> templatePlaceholderNameAndValues)
        {
            foreach (var nAndV in templatePlaceholderNameAndValues)
            {
                string name = nAndV.Name.Replace("{", "").Replace("}", "");
                if (downstreamRequest.Query.Contains(name) && downstreamRequest.Query.Contains(nAndV.Value))
                {
                    int questionMarkOrAmpersand = downstreamRequest.Query.IndexOf(name, StringComparison.Ordinal);
                    downstreamRequest.Query = downstreamRequest.Query.Remove(questionMarkOrAmpersand - 1, 1);
                    Regex rgx = new($@"\b{name}={nAndV.Value}\b");
                    downstreamRequest.Query = rgx.Replace(downstreamRequest.Query, "");
                    if (!string.IsNullOrEmpty(downstreamRequest.Query))
                    {
                        downstreamRequest.Query = string.Concat("?", downstreamRequest.Query.AsSpan(1));
                    }
                }
            }
        }
        /// <summary>
        /// 获取路径
        /// </summary>
        /// <param name="dsPath"></param>
        /// <returns></returns>
        private static string GetPath(DownstreamPath dsPath) => dsPath.Value[..dsPath.Value.IndexOf("?", StringComparison.Ordinal)];
        /// <summary>
        /// 获取查询字符串
        /// </summary>
        /// <param name="dsPath"></param>
        /// <returns></returns>
        private static string GetQueryString(DownstreamPath dsPath) => dsPath.Value[dsPath.Value.IndexOf("?", StringComparison.Ordinal)..];
        /// <summary>
        /// 是否包含查询字符串
        /// </summary>
        /// <param name="dsPath"></param>
        /// <returns></returns>
        private static bool ContainsQueryString(DownstreamPath dsPath) => dsPath.Value.Contains('?');
        /// <summary>
        /// 创建ServiceFabricUri
        /// </summary>
        /// <param name="downstreamRequest"></param>
        /// <param name="downstreamRoute"></param>
        /// <param name="templatePlaceholderNameAndValues"></param>
        /// <param name="dsPath"></param>
        /// <returns></returns>
        private (string path, string query) CreateServiceFabricUri(DownstreamRequest downstreamRequest, DownstreamRoute downstreamRoute, List<PlaceholderNameAndValue> templatePlaceholderNameAndValues, Response<DownstreamPath> dsPath)
        {
            string query = downstreamRequest.Query;
            Response<DownstreamPath> serviceName = replacer.Replace(downstreamRoute.ServiceName, templatePlaceholderNameAndValues);
            string pathTemplate = $"/{serviceName.Data.Value}{dsPath.Data.Value}";
            return (pathTemplate, query);
        }
        /// <summary>
        /// 是否是ServiceFabric请求
        /// </summary>
        /// <param name="config"></param>
        /// <param name="downstreamRoute"></param>
        /// <returns></returns>
        private static bool ServiceFabricRequest(IInternalConfiguration config, DownstreamRoute downstreamRoute) => config.ServiceProviderConfiguration.Type?.ToLower() == "servicefabric" && downstreamRoute.UseServiceDiscovery;
    }
}
