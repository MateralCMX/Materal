using AutoMapper;
using Materal.BaseCore.Common;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Materal.BaseCore.WebAPI.Filters
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        private readonly ILogger<ExceptionFilter> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 发生异常时
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            ResultModel result = GetResult(context);
            JsonResult jsonResult = new(result);
            context.Result = jsonResult;
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private ResultModel GetResult(ExceptionContext context)
        {
            var result = HandlerException(context.Exception);
            if (result != null) return result;
            return HandlerDefaultException(context);
        }
        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static ResultModel? HandlerException(Exception exception)
        {
            if (exception is MateralCoreException || exception is AutoMapperMappingException aex && aex.InnerException != null && aex.InnerException is MateralCoreException)
            {
                return ResultModel.Fail(exception.Message);
            }
            return null;
        }
        /// <summary>
        /// 处理默认异常
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private ResultModel HandlerDefaultException(ExceptionContext context)
        {
            ResultModel result = GetDefaultResult(context.Exception.GetErrorMessage());
            WriteError(context);
            return result;
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static ResultModel GetDefaultResult(string message)
        {
            ResultModel result = MateralCoreConfig.ExceptionConfig.ShowException ? ResultModel.Fail(message) : ResultModel.Fail(MateralCoreConfig.ExceptionConfig.ErrorMessage);
            return result;
        }
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="context"></param>
        private async void WriteError(ExceptionContext context)
        {
            StringBuilder message = new();
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
            Exception exception = new MateralCoreException(message.ToString(), context.Exception);
            _logger.LogError(exception, "服务器发生错误");
        }
    }
}
