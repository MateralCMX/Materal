using Materal.Gateway.OcelotExtension.DownstreamUrlCreator.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Materal.Gateway.OcelotExtension
{
    public static partial class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用异常拦截器
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGatewayDownstreamUrlCreatorMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GatewayDownstreamUrlCreatorMiddleware>();
            return app;
        }
    }
}
