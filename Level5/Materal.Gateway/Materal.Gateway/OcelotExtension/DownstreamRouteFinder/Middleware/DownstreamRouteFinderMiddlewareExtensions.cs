using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension.DownstreamRouteFinder.Middleware
{
    /// <summary>
    /// ����·�ɲ����м����չ
    /// </summary>
    public static class DownstreamRouteFinderMiddlewareExtensions
    {
        /// <summary>
        /// ʹ������·�ɲ����м��
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayDownstreamRouteFinderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GatewayDownstreamRouteFinderMiddleware>();
        }
    }
}
