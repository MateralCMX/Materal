using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.GatewayInterception.Middleware
{
    /// <summary>
    /// 网关拦截中间件扩展
    /// </summary>
    public static class GatewayInterceptionMiddlewareExtensions
    {
        /// <summary>
        /// 使用网关拦截中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayInterceptionMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<GatewayInterceptionMiddleware>();
    }
}
