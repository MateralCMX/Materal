using Microsoft.AspNetCore.Http;

namespace Materal.Extensions.DependencyInjection.AspNetCore
{
    /// <summary>
    /// 替换服务提供者中间件
    /// </summary>
    public class ReplaceServiceProviderMiddleware : IMiddleware
    {
        /// <inheritdoc/>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.RequestServices is not MateralServiceProvider)
            {
                context.RequestServices = new MateralServiceProvider(context.RequestServices);
            }
            await next(context);
        }
    }
}
