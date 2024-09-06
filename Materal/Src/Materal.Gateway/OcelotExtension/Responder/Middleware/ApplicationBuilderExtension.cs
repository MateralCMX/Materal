using Materal.Gateway.OcelotExtension.Responder.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用网关响应器
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayResponderMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GatewayResponderMiddleware>();
            return app;
        }
    }
}
