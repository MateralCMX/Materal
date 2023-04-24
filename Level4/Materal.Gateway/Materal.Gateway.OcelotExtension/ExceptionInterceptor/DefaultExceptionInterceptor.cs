using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Materal.Gateway.OcelotExtension.ExceptionInterceptor
{
    public class DefaultExceptionInterceptor : IExceptionInterceptor
    {
        private readonly ILogger<DefaultExceptionInterceptor> _logger;

        public DefaultExceptionInterceptor(ILogger<DefaultExceptionInterceptor> logger)
        {
            _logger = logger;
        }
        public virtual void HandlerException(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogError(exception, "网关发生错误");
        }
        public virtual Task HandlerExceptionAsync(HttpContext httpContext, Exception exception)
        {
            HandlerException(httpContext, exception);
            return Task.CompletedTask;
        }
    }
}
