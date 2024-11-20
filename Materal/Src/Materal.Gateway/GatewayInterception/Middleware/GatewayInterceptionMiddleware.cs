using Ocelot.Errors;
using Ocelot.Logging;
using Ocelot.Middleware;

namespace Materal.Gateway.GatewayInterception.Middleware
{
    /// <summary>
    /// 网关拦截中间件
    /// </summary>
    public class GatewayInterceptionMiddleware(RequestDelegate next, IOcelotLoggerFactory loggerFactory) : OcelotMiddleware(loggerFactory.CreateLogger<GatewayInterceptionMiddleware>())
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            List<Error> errors = httpContext.Items.Errors();
            if (errors.Count > 0) return;
            await next.Invoke(httpContext);
        }
    }
}
