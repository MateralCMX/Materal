using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Materal.Finance.WebAPI.Filters
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
    public class ExceptionProcessFilter : IExceptionFilter
    {
        /// <summary>
        /// 日志对象
        /// </summary>
        private readonly ILogger<ExceptionProcessFilter> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionProcessFilter(ILogger<ExceptionProcessFilter> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 发生异常时
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogCritical(context.Exception, context.Exception.Message);
            Console.WriteLine(context.Exception.StackTrace);
            ResultModel result = ResultModel.Fail("服务器发生错误,请联系后台工程师处理。");
            context.Result = new JsonResult(result);
        }
    }
}
