using Materal.Gateway.OcelotExtension.ExceptionInterceptor.Middleware;
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
        public static IApplicationBuilder UseGatewayExceptionInterceptorMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GatewayExceptionInterceptorMiddleware>();
            return app;
        }
    }
}
