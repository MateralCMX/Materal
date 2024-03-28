using Materal.Logger.BusLogger;

namespace Materal.Logger.Extensions
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static ILogger Subscribe(this ILogger logger, Action<LogModel> handler, string? busWriterName = null)
        {
            BusLoggerWriter.Subscribe(handler, busWriterName);
            return logger;
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static ILogger Subscribe(this ILogger logger, Func<LogModel, Task> handler, string? busWriterName = null)
        {
            BusLoggerWriter.Subscribe(handler, busWriterName);
            return logger;
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static ILogger Unsubscribe(this ILogger logger, Action<LogModel> handler, string? busWriterName = null)
        {
            BusLoggerWriter.Unsubscribe(handler, busWriterName);
            return logger;
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static ILogger Unsubscribe(this ILogger logger, Func<LogModel, Task> handler, string? busWriterName = null)
        {
            BusLoggerWriter.Unsubscribe(handler, busWriterName);
            return logger;
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        public static ILogger UnsubscribeAll(this ILogger logger)
        {
            BusLoggerWriter.UnsubscribeAll();
            return logger;
        }
    }
}
