using Ocelot.Logging;
using Ocelot.Middleware;

namespace Materal.Gateway.OcelotExtension.ExceptionInterceptor.Middleware
{
    /// <summary>
    /// 网关异常拦截中间件
    /// </summary>
    public class GatewayExceptionInterceptorMiddleware(IOcelotLoggerFactory loggerFactory, RequestDelegate next, IExceptionInterceptor exceptionInterceptor) : OcelotMiddleware(loggerFactory.CreateLogger<GatewayExceptionInterceptorMiddleware>())
    {
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await exceptionInterceptor.HandlerExceptionAsync(httpContext, ex);
            }
        }
    }
}
