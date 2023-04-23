using Microsoft.AspNetCore.Http;
using Ocelot.Logging;
using Ocelot.Middleware;

namespace Materal.Gateway.OcelotExtension.ExceptionInterceptor.Middleware
{
    public class GatewayExceptionInterceptorMiddleware : OcelotMiddleware
    {
        private readonly IExceptionInterceptor _exceptionInterceptor;
        private readonly RequestDelegate _next;
        public GatewayExceptionInterceptorMiddleware(IOcelotLoggerFactory loggerFactory, RequestDelegate next, IExceptionInterceptor exceptionInterceptor) : base(loggerFactory.CreateLogger<GatewayExceptionInterceptorMiddleware>())
        {
            _next = next;
            _exceptionInterceptor = exceptionInterceptor;
        }
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await _exceptionInterceptor.HandlerExceptionAsync(httpContext, ex);
            }
        }
    }
}
