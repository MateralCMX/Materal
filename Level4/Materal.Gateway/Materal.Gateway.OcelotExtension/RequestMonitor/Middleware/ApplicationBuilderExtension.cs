using Materal.Gateway.OcelotExtension.RequestMonitor.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用请求监控
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayRequestMonitorMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GatewayRequestMonitorMiddleware>();
            return app;
        }
    }
}
