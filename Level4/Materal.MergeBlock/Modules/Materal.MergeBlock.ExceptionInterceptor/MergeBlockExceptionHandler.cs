using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// MergeBlock异常处理器
    /// </summary>
    public class MergeBlockExceptionHandler(IOptionsMonitor<ExceptionConfig> exceptionConfig, ILogger<MergeBlockExceptionHandler>? logger = null) : IExceptionHandler
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
            using IDisposable? scope = logger?.BeginScope("MergeBlockException");
            string message = exceptionConfig.CurrentValue.ShowException ? exception.GetErrorMessage() : exceptionConfig.CurrentValue.ErrorMessage;
            ResultModel resultModel = ResultModel.Fail(message);
            message = resultModel.ToJson();
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.Body.WriteAsync(bytes, cancellationToken);
            logger?.LogError(exception, exception.Message);
            return true;
        }
    }
}
