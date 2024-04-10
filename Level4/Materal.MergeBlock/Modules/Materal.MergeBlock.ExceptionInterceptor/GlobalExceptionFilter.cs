using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 全局异常拦截器
    /// </summary>
    public class GlobalExceptionFilter(IOptionsMonitor<ExceptionConfig> exceptionConfig, ILogger<GlobalExceptionFilter>? logger = null) : IExceptionFilter
    {
        /// <summary>
        /// 发生异常时
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            if (exception is MergeBlockModuleException or ValidationException)
            {
                if (exception is AggregateException aggregateException)
                {
                    exception = aggregateException.InnerException ?? exception;
                }
                context.Result = new JsonResult(ResultModel.Fail(exception.Message));
                return;
            }
            using IDisposable? scope = logger?.BeginScope("MergeBlockWebException");
            logger?.LogError(exception, exception.Message);
            string message = exceptionConfig.CurrentValue.ShowException ? exception.GetErrorMessage() : exceptionConfig.CurrentValue.ErrorMessage;
            context.Result = new JsonResult(ResultModel.Fail(message));
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return;
        }
    }
}