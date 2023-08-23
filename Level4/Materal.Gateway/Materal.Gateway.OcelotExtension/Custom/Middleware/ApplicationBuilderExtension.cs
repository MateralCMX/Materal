using Materal.Gateway.Abstractions;
using Materal.Gateway.OcelotExtension.Custom;
using Materal.Gateway.OcelotExtension.Custom.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用自定义中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GatewayCustomMiddleware>();
            return app;
        }
        /// <summary>
        /// 使用网关中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GatewayMiddleware>();
            List<IGatewayMiddleware> gatewayMiddlewares = app.ApplicationServices.GetServices<IGatewayMiddleware>().ToList();
            if (gatewayMiddlewares.Count > 0)
            {
                IGatewayMiddlewareBus gatewayMiddlewareBus = app.ApplicationServices.GetRequiredService<IGatewayMiddlewareBus>();
                foreach (IGatewayMiddleware gatewayMiddleware in gatewayMiddlewares)
                {
                    gatewayMiddlewareBus.Subscribe(gatewayMiddleware.GetType());
                }
            }
            return app;
        }
    }
}
