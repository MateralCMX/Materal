using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Materal.Logger
{
    /// <summary>
    /// 调试Trace监听器
    /// </summary>
    public class DebugTraceListener : TraceListener
    {
        private readonly IServiceProvider _serviceProvider;
        private ILogger? _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DebugTraceListener(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string? message)
        {
            _logger ??= _serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DebugOrTrace");
            _logger.LogDebug(message);
        }

        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string? message) => Write($"{message}\r\n");
    }
}
