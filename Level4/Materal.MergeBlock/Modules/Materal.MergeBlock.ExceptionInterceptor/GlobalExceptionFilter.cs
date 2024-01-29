using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
    public class GlobalExceptionFilter() : IExceptionFilter
    {
        /// <summary>
        /// 发生异常时
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            if(exception is AggregateException aggregateException)
            {
                exception = aggregateException.InnerException ?? exception;
            }
            if (exception is not MergeBlockModuleException && exception is not ValidationException) return;
            ResultModel result = ResultModel.Fail(exception.Message);
            context.Result = new JsonResult(result);
        }
    }
}
