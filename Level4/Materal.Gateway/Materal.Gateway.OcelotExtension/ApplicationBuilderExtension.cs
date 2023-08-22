using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension.DownstreamRouteFinder.Middleware;
using Materal.Gateway.OcelotExtension.WebSockets.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.Authentication.Middleware;
using Ocelot.Authorization.Middleware;
using Ocelot.Cache.Middleware;
using Ocelot.Claims.Middleware;
using Ocelot.Configuration;
using Ocelot.Configuration.Creator;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.DownstreamPathManipulation.Middleware;
using Ocelot.DownstreamUrlCreator.Middleware;
using Ocelot.Errors;
using Ocelot.Headers.Middleware;
using Ocelot.LoadBalancer.Middleware;
using Ocelot.Logging;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using Ocelot.QueryStrings.Middleware;
using Ocelot.RateLimit.Middleware;
using Ocelot.Request.Middleware;
using Ocelot.RequestId.Middleware;
using Ocelot.Responses;
using Ocelot.Security.Middleware;
using System.Diagnostics;
using System.Text;

namespace Materal.Gateway.OcelotExtension
{
    /// <summary>
    /// 应用程序构建器扩展
    /// </summary>
    public static partial class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用Ocelot网关
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="ignoreUnableToFindDownstreamRouteError"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> UseOcelotGatewayAsync(this IApplicationBuilder builder, bool ignoreUnableToFindDownstreamRouteError = false)
        {
            ApplicationConfig.IgnoreUnableToFindDownstreamRouteError = ignoreUnableToFindDownstreamRouteError;
            await CreateConfigurationAsync(builder);
            ConfigureDiagnosticListener(builder);
            return CreateOcelotPipeline(builder);
        }
        private static IApplicationBuilder CreateOcelotPipeline(IApplicationBuilder builder)
        {
            builder.BuildOcelotGatewayPipeline();
            builder.Properties["analysis.NextMiddlewareName"] = "TransitionToOcelotMiddleware";
            return builder;
        }
        private static GatewayException GetStopOcelotStartingException(Response? config)
        {
            StringBuilder errorMessage = new();
            errorMessage.Append("Ocelot启动失败");
            if (config != null && config.Errors.Count > 0)
            {
                errorMessage.Append($"错误组: {string.Join(",", config.Errors.Select((Error x) => x.ToString()))}");
            }
            return new GatewayException(errorMessage.ToString());
        }
        private static async Task<IInternalConfiguration> CreateConfigurationAsync(IApplicationBuilder builder)
        {
            OcelotService.Service = builder.ApplicationServices;
            IOptionsMonitor<FileConfiguration> fileConfig = OcelotService.GetService<IOptionsMonitor<FileConfiguration>>();
            IInternalConfigurationCreator internalConfigCreator = OcelotService.GetService<IInternalConfigurationCreator>();
            Response<IInternalConfiguration> response = await internalConfigCreator.Create(fileConfig.CurrentValue);
            if (response.IsError) throw GetStopOcelotStartingException(response);
            IInternalConfigurationRepository internalConfigRepo = OcelotService.GetService<IInternalConfigurationRepository>();
            internalConfigRepo.AddOrReplace(response.Data);
            fileConfig.OnChange(async delegate (FileConfiguration config)
            {
                Response<IInternalConfiguration> response2 = await internalConfigCreator.Create(config);
                internalConfigRepo.AddOrReplace(response2.Data);
            });
            IEnumerable<OcelotMiddlewareConfigurationDelegate> services = OcelotService.GetServices<OcelotMiddlewareConfigurationDelegate>();
            foreach (OcelotMiddlewareConfigurationDelegate item in services)
            {
                await item(builder);
            }
            return GetOcelotConfig(internalConfigRepo);
        }
        /// <summary>
        /// 获得Ocelot配置
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        private static IInternalConfiguration GetOcelotConfig(IInternalConfigurationRepository provider)
        {
            Response<IInternalConfiguration> response = provider.Get();
            if (response?.Data == null || response.IsError) throw GetStopOcelotStartingException(response);
            return response.Data;
        }
        /// <summary>
        /// 配置诊断监听
        /// </summary>
        /// <param name="builder"></param>
        private static void ConfigureDiagnosticListener(IApplicationBuilder builder)
        {
            ServiceProviderServiceExtensions.GetService<IWebHostEnvironment>(builder.ApplicationServices);
            OcelotDiagnosticListener service = OcelotService.GetService<OcelotDiagnosticListener>();
            ServiceProviderServiceExtensions.GetService<DiagnosticListener>(builder.ApplicationServices).SubscribeWithAdapter(service);
        }
        /// <summary>
        /// 创建Ocelot网关管道
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        private static IApplicationBuilder BuildOcelotGatewayPipeline(this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.UseGatewayExceptionInterceptorMiddleware();
            app.UseDownstreamContextMiddleware();
            app.MapWhen((HttpContext httpContext) => httpContext.WebSockets.IsWebSocketRequest, wenSocketsApp =>
            {
                wenSocketsApp.UseGatewayDownstreamRouteFinderMiddleware();
                wenSocketsApp.UseMultiplexingMiddleware();
                wenSocketsApp.UseDownstreamRequestInitialiser();
                wenSocketsApp.UseLoadBalancingMiddleware();
                wenSocketsApp.UseDownstreamUrlCreatorMiddleware();
                wenSocketsApp.UseGatewayWebSocketsProxyMiddleware();
            });
            app.UseGatewayResponderMiddleware();
            app.UseGatewayDownstreamRouteFinderMiddleware();
            app.MapWhen((HttpContext httpContext) =>
            {
                if (!ApplicationConfig.IgnoreUnableToFindDownstreamRouteError) return true;
                List<Error> errors = httpContext.Items.Errors();
                if (errors.Count != 1 || errors.First() is UnableToFindDownstreamRouteError) return true;
                httpContext.Items.Remove("Errors");
                return false;

            }, gatewayApp =>
            {
                gatewayApp.UseMultiplexingMiddleware();
                gatewayApp.UseSecurityMiddleware();
                gatewayApp.UseHttpHeadersTransformationMiddleware();
                gatewayApp.UseDownstreamRequestInitialiser();
                gatewayApp.UseRateLimiting();
                gatewayApp.UseRequestIdMiddleware();
                gatewayApp.UseAuthenticationMiddleware();
                gatewayApp.UseClaimsToClaimsMiddleware();
                gatewayApp.UseAuthorizationMiddleware();
                gatewayApp.UseClaimsToHeadersMiddleware();
                gatewayApp.UseClaimsToQueryStringMiddleware();
                gatewayApp.UseClaimsToDownstreamPathMiddleware();
                gatewayApp.UseLoadBalancingMiddleware();
                gatewayApp.UseGatewayDownstreamUrlCreatorMiddleware();
                gatewayApp.UseGatewayRequestMonitorMiddleware();
                gatewayApp.UseOutputCacheMiddleware();
                gatewayApp.UseGatewayCustomMiddleware();
                gatewayApp.UseGatewayHttpRequesterMiddleware();
            });
            return app;
        }
    }
}
