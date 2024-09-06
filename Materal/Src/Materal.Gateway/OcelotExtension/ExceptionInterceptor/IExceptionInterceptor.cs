namespace Materal.Gateway.OcelotExtension.ExceptionInterceptor
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
    public interface IExceptionInterceptor
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        Task HandlerExceptionAsync(HttpContext httpContext, Exception exception);
    }
}
