using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.CustomGateway.Middleware
{
    /// <summary>
    /// 网关拦截中间件扩展
    /// </summary>
    public static class CustomGatewayMiddlewareExtensions
    {
        /// <summary>
        /// 使用网关拦截中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomGatewayMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<CustomGatewayMiddleware>();
    }
}
