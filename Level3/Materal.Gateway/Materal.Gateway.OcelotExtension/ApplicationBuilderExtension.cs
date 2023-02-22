using Materal.Gateway.Common;
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
using Ocelot.DownstreamRouteFinder.Middleware;
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
using Ocelot.WebSockets.Middleware;
using System.Diagnostics;
using System.Text;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> UseOcelotGateway(this IApplicationBuilder builder)
        {
            await CreateConfiguration(builder);
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
            if(config != null && config.Errors.Count > 0)
            {
                errorMessage.Append($"错误组: {string.Join(",", config.Errors.Select((Error x) => x.ToString()))}");
            }
            return new GatewayException(errorMessage.ToString());
        }
        private static async Task<IInternalConfiguration> CreateConfiguration(IApplicationBuilder builder)
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
        /// <param name="pipelineConfiguration"></param>
        /// <returns></returns>
        private static RequestDelegate BuildOcelotGatewayPipeline(this IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.UseGatewayExceptionInterceptorMiddleware();
            app.UseDownstreamContextMiddleware();
            app.MapWhen((HttpContext httpContext) => httpContext.WebSockets.IsWebSocketRequest, delegate (IApplicationBuilder wenSocketsApp)
            {
                wenSocketsApp.UseDownstreamRouteFinderMiddleware();
                wenSocketsApp.UseMultiplexingMiddleware();
                wenSocketsApp.UseDownstreamRequestInitialiser();
                wenSocketsApp.UseLoadBalancingMiddleware();
                wenSocketsApp.UseDownstreamUrlCreatorMiddleware();
                wenSocketsApp.UseWebSocketsProxyMiddleware();
            });
            app.UseGatewayResponderMiddleware();
            app.UseDownstreamRouteFinderMiddleware();
            app.UseMultiplexingMiddleware();
            app.UseSecurityMiddleware();
            app.UseHttpHeadersTransformationMiddleware();
            app.UseDownstreamRequestInitialiser();
            app.UseRateLimiting();
            app.UseRequestIdMiddleware();
            app.UseAuthenticationMiddleware();
            app.UseClaimsToClaimsMiddleware();
            app.UseAuthorizationMiddleware();
            app.UseClaimsToHeadersMiddleware();
            app.UseClaimsToQueryStringMiddleware();
            app.UseClaimsToDownstreamPathMiddleware();
            app.UseLoadBalancingMiddleware();
            app.UseGatewayDownstreamUrlCreatorMiddleware();
            app.UseGatewayRequestMonitorMiddleware();
            app.UseOutputCacheMiddleware();
            app.UseGatewayCustomMiddleware();
            app.UseGatewayHttpRequesterMiddleware();
            return app.Build();
        }
    }
}
