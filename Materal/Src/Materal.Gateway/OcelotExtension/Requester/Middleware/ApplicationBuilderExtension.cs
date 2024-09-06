using Materal.Gateway.OcelotExtension.Requester.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用网关请求器
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayHttpRequesterMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GatewayHttpRequesterMiddleware>();
            return app;
        }
    }
}
