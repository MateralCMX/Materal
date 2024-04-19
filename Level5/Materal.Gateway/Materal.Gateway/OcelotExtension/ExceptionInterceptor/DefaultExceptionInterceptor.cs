namespace Materal.Gateway.OcelotExtension.ExceptionInterceptor
{
    /// <summary>
    /// 默认异常拦截器
    /// </summary>
    public class DefaultExceptionInterceptor(ILogger<DefaultExceptionInterceptor> logger) : IExceptionInterceptor
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public virtual async Task HandlerExceptionAsync(HttpContext httpContext, Exception exception)
        {
            string errorMessage = exception.GetErrorMessage();
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/text";
            await httpContext.Response.WriteAsync(errorMessage);
            logger.LogError(exception, "网关发生错误");
        }
    }
}
