using Materal.MergeBlock.Web.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace Materal.MergeBlock.ExceptionInterceptor
{
    /// <summary>
    /// 全局异常拦截器
    /// </summary>
    public class GlobalExceptionFilter(IOptionsMonitor<ExceptionOptions> exceptionConfig, ILogger<GlobalExceptionFilter>? logger = null) : IAsyncExceptionFilter
    {
        /// <summary>
        /// 发生异常时
        /// </summary>
        /// <param name="context"></param>
        public async Task OnExceptionAsync(ExceptionContext context)
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
            else if (exception is HttpCodeException httpCodeException)
            {
                context.Result = new JsonResult(ResultModel.Fail(httpCodeException.Message));
                context.HttpContext.Response.StatusCode = httpCodeException.HttpCode;
                return;
            }
            using IDisposable? scope = logger?.BeginScope("MergeBlockWebException");
            string errorMessage = await GetErrorMessageAsync(context, exception);
            logger?.LogError(exception, errorMessage);
            string message = exceptionConfig.CurrentValue.ShowException ? exception.Message : exceptionConfig.CurrentValue.ErrorMessage;
            context.Result = new JsonResult(ResultModel.Fail(message));
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return;
        }
        private static async Task<string> GetErrorMessageAsync(ExceptionContext context, Exception exception)
        {
            StringBuilder message = new();
            message.AppendLine(exception.Message);
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                message.AppendLine($"Controller:{actionDescriptor.ControllerName}");
                message.AppendLine($"Action:{actionDescriptor.ActionName}");
                string? ipAddress = FilterHelper.GetIPAddress(context.HttpContext.Connection);
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    message.AppendLine($"ClientIP:{ipAddress}");
                }
                Guid? loginUserID = FilterHelper.GetOperatingUserID(context.HttpContext.User);
                if (loginUserID.HasValue)
                {
                    message.AppendLine($"UserID:{loginUserID.Value}");
                }
                string? paramsValue = await FilterHelper.GetRequestContentAsync(context.HttpContext.Request);
                if (!string.IsNullOrWhiteSpace(paramsValue))
                {
                    message.AppendLine(paramsValue);
                }
            }
            return message.ToString();
        }
    }
}