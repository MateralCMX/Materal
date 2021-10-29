using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using AspectCore.DynamicProxy;
using Materal.APP.Core;
using Materal.APP.Core.Models;
using Materal.Model;
using Materal.NetworkHelper;
using Microsoft.AspNetCore.Http;

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
            JsonResult jsonResult = new JsonResult(result);
            context.Result = jsonResult;
        }
        /// <summary>
        /// 获得返回
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private ResultModel GetResult(ExceptionContext context)
        {
            string message = GetErrorMessage(context.Exception);
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
        /// <summary>
        /// 处理默认异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
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
            _logger.LogError(context.Exception, message);
        }
        /// <summary>
        /// 获得消息
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string GetErrorMessage(Exception exception)
        {
            string message = $"{exception.Message}\r\n{exception.StackTrace}\r\n";
            switch (exception)
            {
                case AggregateException aggregateException:
                {
                    message = aggregateException.InnerExceptions.Aggregate(message, (current, ex) => current + GetErrorMessage(ex));
                    break;
                }
                case MateralHttpException httpException:
                    message = httpException.GetMessage();
                    break;
                case MateralAPPException materalAPPException:
                    message = materalAPPException.Message;
                    break;
                case AspectInvocationException aspectInvocationException:
                    message = aspectInvocationException.InnerException is MateralAPPException ?
                        aspectInvocationException.InnerException?.Message :
                        GetErrorMessage(aspectInvocationException.InnerException);
                    break;
                default:
                {
                    message = string.Empty; 
                    Exception tempException = exception;
                    do
                    {
                        message += $"{tempException.Message}\r\n{tempException.StackTrace}\r\n";
                    } while (tempException.InnerException != null);
                    break;
                }
            }
            return message;
        }
    }
}
