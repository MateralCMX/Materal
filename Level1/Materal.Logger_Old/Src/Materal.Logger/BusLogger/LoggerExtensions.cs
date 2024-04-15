namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="monitor"></param>
        /// <param name="busName"></param>
        public static ILogger Subscribe(this ILogger logger, ILogMonitor monitor, string? busName = null)
        {
            BusLoggerWriter.Subscribe(monitor, busName);
            return logger;
        }
        /// <summary>
        /// 能否订阅
        /// </summary>
        /// <param name="_"></param>
        /// <param name="monitor"></param>
        /// <param name="busName"></param>
        public static bool CanSubscribe(this ILogger _, ILogMonitor monitor, string? busName = null) 
            => BusLoggerWriter.CanSubscribe(monitor, busName);
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="monitor"></param>
        /// <param name="busName"></param>
        public static ILogger Unsubscribe(this ILogger logger, ILogMonitor monitor, string? busName = null)
        {
            BusLoggerWriter.Unsubscribe(monitor, busName);
            return logger;
        }
        /// <summary>
        /// 取消所有订阅
        /// </summary>
        /// <param name="logger"></param>
        public static ILogger UnsubscribeAll(this ILogger logger)
        {
            BusLoggerWriter.UnsubscribeAll();
            return logger;
        }
    }
}
