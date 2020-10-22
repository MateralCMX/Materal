using AspectCore.DynamicProxy;
using Materal.APP.Core;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using System;

namespace Materal.APP.WebAPICore.Filters
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
            var jsonResult = new JsonResult(result);
            context.Result = jsonResult;
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private ResultModel GetResult(ExceptionContext context)
        {
            string message = ConsoleHelperBase.GetMessage(context.Exception);
            ResultModel result = context.Exception switch
            {
                MateralAPPException _ => ResultModel.Fail(message),
                AspectInvocationException aspectInvocationException =>
                aspectInvocationException.InnerException is MateralAPPException
                    ? ResultModel.Fail(message)
                    : HandlerDefaultException(context, message),
                _ => HandlerDefaultException(context, message)
            };
            return result;
        }

        private ResultModel HandlerDefaultException(ExceptionContext context, string message)
        {
            ResultModel result = GetResult(message);
            WriteError(context, message);
            return result;
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private ResultModel GetResult(string message)
        {
            ResultModel result = ApplicationConfig.ShowException ? ResultModel.Fail(message) : ResultModel.Fail("服务器发生错误,请联系后台工程师处理。");
            return result;
        }
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        private void WriteError(ExceptionContext context, string message)
        {
            if (context.Exception is BadHttpRequestException) return;
            _logger.LogCritical(context.Exception, message);
            ConsoleHelperBase.WriteLine(nameof(ExceptionFilter), message, "Error", ConsoleColor.Red);
        }
    }
}
