using Materal.Gateway.OcelotExtension.ExceptionInterceptor;

namespace Materal.Gateway.WebAPI
{
    /// <summary>
    /// Materal网关异常拦截器
    /// </summary>
    /// <param name="logger"></param>
    public class MateralGatewayExceptionInterceptor(ILogger<MateralGatewayExceptionInterceptor> logger) : DefaultExceptionInterceptor(logger), IExceptionInterceptor
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task HandlerExceptionAsync(HttpContext httpContext, Exception exception)
        {
            if (exception.Source is not null && exception.Source.StartsWith("MMLib.SwaggerForOcelot"))
            {
                logger.LogWarning(exception, "Swagger配置有误");
            }
            else
            {
                await base.HandlerExceptionAsync(httpContext, exception);
            }
        }
    }
}
