using System.Diagnostics;

namespace Materal.Logger
{
    /// <summary>
    /// 调试Trace监听器
    /// </summary>
    public class DebugTraceListener(IServiceProvider serviceProvider) : TraceListener
    {
        private ILogger? _logger;
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="message"></param>
        public override void Write(string? message)
        {
            _logger ??= serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DebugOrTrace");
            _logger.LogDebug(message);
        }

        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="message"></param>
        public override void WriteLine(string? message) => Write($"{message}\r\n");
    }
}
