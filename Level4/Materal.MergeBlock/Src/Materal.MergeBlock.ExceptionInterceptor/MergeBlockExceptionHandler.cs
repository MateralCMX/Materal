using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// MergeBlock异常处理器
    /// </summary>
    public class MergeBlockExceptionHandler(IOptionsMonitor<ExceptionConfig> exceptionConfig) : IExceptionHandler
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            string message = exceptionConfig.CurrentValue.ShowException ? exception.GetExceptionMessage() : exceptionConfig.CurrentValue.ErrorMessage;
            ResultModel resultModel = ResultModel.Fail(message);
            message = resultModel.ToJson();
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.Body.WriteAsync(bytes, cancellationToken);
            return true;
        }
    }
}
