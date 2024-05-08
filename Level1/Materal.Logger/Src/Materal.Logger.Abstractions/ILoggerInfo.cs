namespace Materal.Logger.Abstractions
{
    /// <summary>
    /// 日志主机日志记录器
    /// </summary>
    public interface ILoggerInfo
    {
        /// <summary>
        /// 日志选项
        /// </summary>
        LoggerOptions Options { get; set; }
        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        Task StopAsync();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="consoleColor"></param>
        void Log(string message, LogLevel logLevel, ConsoleColor consoleColor = ConsoleColor.Gray);
        /// <summary>
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        void LogInfomation(string message);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        void LogWarning(string message);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void LogWarning(string message, Exception? exception);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void LogError(string message, Exception? exception);
    }
}
