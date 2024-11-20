using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.DownstreamRouteFinder.Middleware
{
    /// <summary>
    /// 下游路由查找中间件扩展
    /// </summary>
    public static class GatewayDownstreamRouteFinderMiddlewareExtensions
    {
        /// <summary>
        /// 使用网关下游路由查找中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayDownstreamRouteFinderMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<GatewayDownstreamRouteFinderMiddleware>();
    }
}
