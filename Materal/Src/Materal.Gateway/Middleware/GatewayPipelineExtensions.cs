using Materal.Gateway.CustomGateway.Middleware;
using Materal.Gateway.DownstreamRouteFinder.Middleware;
using Materal.Gateway.GatewayInterception.Middleware;
using Microsoft.AspNetCore.Builder;
using Ocelot.Authentication.Middleware;
using Ocelot.Authorization.Middleware;
using Ocelot.Cache.Middleware;
using Ocelot.Claims.Middleware;
using Ocelot.DownstreamPathManipulation.Middleware;
using Ocelot.DownstreamRouteFinder.Finder;
using Ocelot.DownstreamUrlCreator.Middleware;
using Ocelot.Errors;
using Ocelot.Errors.Middleware;
using Ocelot.Headers.Middleware;
using Ocelot.LoadBalancer.Middleware;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using Ocelot.QueryStrings.Middleware;
using Ocelot.RateLimiting.Middleware;
using Ocelot.Request.Middleware;
using Ocelot.Requester.Middleware;
using Ocelot.RequestId.Middleware;
using Ocelot.Responder.Middleware;
using Ocelot.Security.Middleware;
using Ocelot.WebSockets;

namespace Materal.Gateway.Middleware
{
    /// <summary>
    /// 网关管道扩展
    /// </summary>
    public static class GatewayPipelineExtensions
    {
        /// <summary>
        /// 构建网关管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="pipelineConfiguration"></param>
        /// <param name="gatewayInterception"></param>
        /// <returns></returns>
        internal static void UseGatewayPipeline(this IApplicationBuilder app, OcelotPipelineConfiguration pipelineConfiguration, bool gatewayInterception)
        {
            bool CanGatewayHandler(HttpContext httpContext)
            {
                if (gatewayInterception) return true;
                List<Error> errors = httpContext.Items.Errors();
                if (errors.Count != 1 || errors.First() is not UnableToFindDownstreamRouteError) return true;
                httpContext.Items.Remove("Errors");
                return false;
            }
            app.UseCustomGatewayMiddleware();
            app.UseDownstreamContextMiddleware();
            app.UseExceptionHandlerMiddleware();
            app.MapWhen(httpContext => httpContext.WebSockets.IsWebSocketRequest, wenSocketsApp =>
            {
                wenSocketsApp.UseGatewayDownstreamRouteFinderMiddleware();
                wenSocketsApp.MapWhen(CanGatewayHandler, gatewayApp =>
                {
                    gatewayApp.UseGatewayInterceptionMiddleware();
                    gatewayApp.UseMultiplexingMiddleware();
                    gatewayApp.UseDownstreamRequestInitialiser();
                    gatewayApp.UseLoadBalancingMiddleware();
                    gatewayApp.UseDownstreamUrlCreatorMiddleware();
                    gatewayApp.UseWebSocketsProxyMiddleware();
                });
            });
            app.UseIfNotNull(pipelineConfiguration.PreErrorResponderMiddleware);
            app.UseResponderMiddleware();
            app.UseGatewayDownstreamRouteFinderMiddleware();
            app.MapWhen(CanGatewayHandler, gatewayApp =>
            {
                gatewayApp.UseGatewayInterceptionMiddleware();
                gatewayApp.UseMultiplexingMiddleware();
                gatewayApp.UseSecurityMiddleware();
                if (pipelineConfiguration.MapWhenOcelotPipeline != null)
                {
                    foreach (var pipeline in pipelineConfiguration.MapWhenOcelotPipeline)
                    {
                        gatewayApp.MapWhen(pipeline.Key, pipeline.Value);
                    }
                }
                gatewayApp.UseHttpHeadersTransformationMiddleware();
                gatewayApp.UseDownstreamRequestInitialiser();
                gatewayApp.UseRateLimiting();
                gatewayApp.UseRequestIdMiddleware();
                gatewayApp.UseIfNotNull(pipelineConfiguration.PreAuthenticationMiddleware);
                gatewayApp.UseIfNotNull<AuthenticationMiddleware>(pipelineConfiguration.AuthenticationMiddleware);
                gatewayApp.UseClaimsToClaimsMiddleware();
                gatewayApp.UseIfNotNull(pipelineConfiguration.PreAuthorizationMiddleware);
                gatewayApp.UseIfNotNull<AuthorizationMiddleware>(pipelineConfiguration.AuthorizationMiddleware);
                gatewayApp.UseIfNotNull<ClaimsToHeadersMiddleware>(pipelineConfiguration.ClaimsToHeadersMiddleware);
                gatewayApp.UseIfNotNull(pipelineConfiguration.PreQueryStringBuilderMiddleware);
                gatewayApp.UseClaimsToQueryStringMiddleware();
                gatewayApp.UseClaimsToDownstreamPathMiddleware();
                gatewayApp.UseLoadBalancingMiddleware();
                gatewayApp.UseDownstreamUrlCreatorMiddleware();
                gatewayApp.UseOutputCacheMiddleware();
                gatewayApp.UseHttpRequesterMiddleware();
            });
        }
        private static IApplicationBuilder UseIfNotNull(this IApplicationBuilder builder, Func<HttpContext, Func<Task>, Task> middleware)
            => middleware != null ? builder.Use(middleware) : builder;
        private static IApplicationBuilder UseIfNotNull<TMiddleware>(this IApplicationBuilder builder, Func<HttpContext, Func<Task>, Task> middleware)
            where TMiddleware : OcelotMiddleware => middleware != null
                ? builder.Use(middleware)
                : builder.UseMiddleware<TMiddleware>();
    }
}
