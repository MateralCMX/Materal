using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Materal.Gateway.OcelotExtension.ExceptionInterceptor
{
    /// <summary>
    /// 默认异常拦截器
    /// </summary>
    public class DefaultExceptionInterceptor : IExceptionInterceptor
    {
        private readonly ILogger<DefaultExceptionInterceptor> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        public DefaultExceptionInterceptor(ILogger<DefaultExceptionInterceptor> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        public virtual void HandlerException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogError(exception, "网关发生错误");
        }
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public virtual Task HandlerExceptionAsync(HttpContext httpContext, Exception exception)
        {
            HandlerException(httpContext, exception);
            return Task.CompletedTask;
        }
    }
}
