using Materal.Gateway.OcelotExtension.Custom.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用自定义中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayCustomMiddleware(this IApplicationBuilder app) => app.UseMiddleware<GatewayCustomMiddleware>();
        /// <summary>
        /// 使用网关中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayMiddleware(this IApplicationBuilder app) => app.UseMiddleware<GatewayMiddleware>();
    }
}
