using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension.DownstreamRouteFinder.Middleware
{
    /// <summary>
    /// 下游路由查找中间件扩展
    /// </summary>
    public static class DownstreamRouteFinderMiddlewareExtensions
    {
        /// <summary>
        /// 使用下游路由查找中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayDownstreamRouteFinderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GatewayDownstreamRouteFinderMiddleware>();
        }
    }
}
