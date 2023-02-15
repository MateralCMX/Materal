using Materal.Gateway.OcelotExtension.Middleware;
using Microsoft.AspNetCore.Http;
using Ocelot.Configuration;
using Ocelot.DownstreamRouteFinder.UrlMatcher;
using Ocelot.DownstreamUrlCreator.UrlTemplateReplacer;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Request.Middleware;
using Ocelot.Responses;
using Ocelot.Values;
using System.Text.RegularExpressions;

namespace Materal.Gateway.OcelotExtension.DownstreamUrlCreator.Middleware
{
    public class GatewayDownstreamUrlCreatorMiddleware : OcelotMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDownstreamPathPlaceholderReplacer _replacer;
        public GatewayDownstreamUrlCreatorMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory, IDownstreamPathPlaceholderReplacer replacer) : base(loggerFactory.CreateLogger<GatewayDownstreamUrlCreatorMiddleware>())
        {
            _next = next;
            _replacer = replacer;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            DownstreamRoute downstreamRoute = httpContext.Items.DownstreamRoute();
            List<PlaceholderNameAndValue> templatePlaceholderNameAndValues = httpContext.Items.TemplatePlaceholderNameAndValues();
            Response<DownstreamPath> response = _replacer.Replace(downstreamRoute.DownstreamPathTemplate.Value, templatePlaceholderNameAndValues);
            DownstreamRequest downstreamRequest = httpContext.Items.DownstreamRequest();
            if (response.IsError)
            {
                Logger.LogDebug("IDownstreamPathPlaceholderReplacer returned an error, setting pipeline error");
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
            Logger.LogDebug($"Downstream url is {downstreamRequest}");
            await _next.Invoke(httpContext);
        }
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
        private static string GetPath(DownstreamPath dsPath) => dsPath.Value[..dsPath.Value.IndexOf("?", StringComparison.Ordinal)];
        private static string GetQueryString(DownstreamPath dsPath) => dsPath.Value[dsPath.Value.IndexOf("?", StringComparison.Ordinal)..];
        private static bool ContainsQueryString(DownstreamPath dsPath) => dsPath.Value.Contains('?');
        private (string path, string query) CreateServiceFabricUri(DownstreamRequest downstreamRequest, DownstreamRoute downstreamRoute, List<PlaceholderNameAndValue> templatePlaceholderNameAndValues, Response<DownstreamPath> dsPath)
        {
            string query = downstreamRequest.Query;
            Response<DownstreamPath> serviceName = _replacer.Replace(downstreamRoute.ServiceName, templatePlaceholderNameAndValues);
            string pathTemplate = $"/{serviceName.Data.Value}{dsPath.Data.Value}";
            return (pathTemplate, query);
        }
        private static bool ServiceFabricRequest(IInternalConfiguration config, DownstreamRoute downstreamRoute) => config.ServiceProviderConfiguration.Type?.ToLower() == "servicefabric" && downstreamRoute.UseServiceDiscovery;
    }
}
