using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension.WebSockets.Middleware
{
    /// <summary>
    /// ����WebSockets�����м����չ
    /// </summary>
    public static class WebSocketsProxyMiddlewareExtensions
    {
        /// <summary>
        /// ʹ������WebSockets�����м��
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayWebSocketsProxyMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<GatewayWebSocketsProxyMiddleware>();
    }
}
