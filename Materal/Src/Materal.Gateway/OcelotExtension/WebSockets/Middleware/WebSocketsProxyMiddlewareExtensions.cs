using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension.WebSockets.Middleware
{
    /// <summary>
    /// 网关WebSockets代理中间件扩展
    /// </summary>
    public static class WebSocketsProxyMiddlewareExtensions
    {
        /// <summary>
        /// 使用网关WebSockets代理中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayWebSocketsProxyMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<GatewayWebSocketsProxyMiddleware>();
    }
}
