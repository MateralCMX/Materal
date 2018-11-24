using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Materal.ApplicationUpdate.WebAPI
{
    /// <inheritdoc />
    /// <summary>
    /// 异常处理过滤器
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
        public ExceptionProcessFilter(ILogger<ExceptionProcessFilter> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        /// <summary>
        /// 发生错误
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
        }
    }
}
