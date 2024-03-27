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
        public static ILogger SubscribeLogger(this ILogger logger, Action<BusLoggerWriterModel[]> handler, string? busWriterName = null)
        {
            BusLoggerWriter.SubscribeLogger(handler, busWriterName);
            return logger;
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static ILogger SubscribeLogger(this ILogger logger, Func<BusLoggerWriterModel[], Task> handler, string? busWriterName = null)
        {
            BusLoggerWriter.SubscribeLogger(handler, busWriterName);
            return logger;
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static ILogger UnsubscribeLogger(this ILogger logger, Action<BusLoggerWriterModel[]> handler, string? busWriterName = null)
        {
            BusLoggerWriter.UnsubscribeLogger(handler, busWriterName);
            return logger;
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static ILogger UnsubscribeLogger(this ILogger logger, Func<BusLoggerWriterModel[], Task> handler, string? busWriterName = null)
        {
            BusLoggerWriter.UnsubscribeLogger(handler, busWriterName);
            return logger;
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="logger"></param>
        public static ILogger UnsubscribeAllLogger(this ILogger logger)
        {
            BusLoggerWriter.UnsubscribeAllLogger();
            return logger;
        }
    }
}
