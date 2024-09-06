using Materal.Gateway.Abstractions;
using Ocelot.Logging;
using Ocelot.Middleware;

namespace Materal.Gateway.OcelotExtension.Custom.Middleware
{
    /// <summary>
    /// 网关中间件
    /// </summary>
    public class GatewayMiddleware(IServiceProvider serviceProvider, IOcelotLoggerFactory loggerFactory, RequestDelegate next) : OcelotMiddleware(loggerFactory.CreateLogger<GatewayCustomMiddleware>())
    {
        /// <summary>
        /// 中间件执行
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            List<IGatewayMiddleware> gatewayMiddlewares = [.. scope.ServiceProvider.GetServices<IGatewayMiddleware>().OrderBy(m => m.Index)];
            Logger.LogDebug($"共找到网关中间件{gatewayMiddlewares.Count}个");
            foreach (IGatewayMiddleware gatewayMiddleware in gatewayMiddlewares)
            {
                Type type = gatewayMiddleware.GetType();
                Logger.LogDebug($"网关中间件[{type.FullName}]执行");
                try
                {
                    await gatewayMiddleware.InvokeAsync(httpContext);
                }
                catch (Exception ex)
                {
                    Logger.LogError($"网关中间件[{type.FullName}]执行异常", ex);
                    return;
                }
                if (httpContext.Response.HasStarted)
                {
                    Logger.LogDebug($"网关中间件[{type.FullName}]执行结果:拦截请求");
                    return;
                }
                else
                {
                    Logger.LogDebug($"网关中间件[{type.FullName}]执行结果:请求继续");
                }
            }
            await next.Invoke(httpContext);
        }
    }
}
