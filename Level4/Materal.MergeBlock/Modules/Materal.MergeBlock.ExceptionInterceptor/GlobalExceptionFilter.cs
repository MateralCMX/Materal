using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.ExceptionInterceptor
{
#if NET8_0
    /// <summary>
    /// 全局异常拦截器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 发生异常时
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            if (exception is not MergeBlockModuleException && exception is not ValidationException) return;
            if (exception is AggregateException aggregateException)
            {
                exception = aggregateException.InnerException ?? exception;
            }
            ResultModel result = ResultModel.Fail(exception.Message);
            context.Result = new JsonResult(result);
        }
    }
#else
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
            ResultModel result;
            if (exception is not MergeBlockModuleException && exception is not ValidationException)
            {
                using IDisposable? scope = logger?.BeginScope("MergeBlockException");
                logger?.LogError(exception, exception.Message);
                string message = exceptionConfig.CurrentValue.ShowException ? exception.GetErrorMessage() : exceptionConfig.CurrentValue.ErrorMessage;
                result = ResultModel.Fail(message);
            }
            else
            {
                if (exception is AggregateException aggregateException)
                {
                    exception = aggregateException.InnerException ?? exception;
                }
                result = ResultModel.Fail(exception.Message);
            }
            context.Result = new JsonResult(result);
        }
    }
#endif
}