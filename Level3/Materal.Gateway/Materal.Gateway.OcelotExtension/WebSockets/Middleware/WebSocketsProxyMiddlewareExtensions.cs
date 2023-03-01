using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension.WebSockets.Middleware
{
    public static class WebSocketsProxyMiddlewareExtensions
    {
        public static IApplicationBuilder UseGatewayWebSocketsProxyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GatewayWebSocketsProxyMiddleware>();
        }
    }
}
