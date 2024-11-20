using Microsoft.AspNetCore.Builder;
using Ocelot.Middleware;

namespace Materal.Gateway.Middleware
{
    /// <summary>
    /// 网关中间件扩展
    /// </summary>
    public static class GatewayMiddlewareExtensions
    {
        /// <summary>
        /// 使用网关
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="gatewayInterception"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> UseGateway(this IApplicationBuilder builder, bool gatewayInterception = false)
        {
            await builder.UseOcelot((builder, config) => builder.UseGatewayPipeline(config, gatewayInterception));
            return builder;
        }
    }
}
