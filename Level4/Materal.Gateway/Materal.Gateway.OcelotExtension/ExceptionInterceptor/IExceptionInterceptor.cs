using Microsoft.AspNetCore.Http;

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
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        void HandlerException(HttpContext httpContext, Exception exception);
    }
}
