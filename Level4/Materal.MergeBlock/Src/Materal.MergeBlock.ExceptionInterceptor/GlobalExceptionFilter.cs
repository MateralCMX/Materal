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
            if (context.Exception is not MergeBlockModuleException && context.Exception is not ValidationException) return;
            ResultModel result = ResultModel.Fail(context.Exception.Message);
            context.Result = new JsonResult(result);
        }
    }
}
