namespace Materal.Logger.LoggerLogs
{
    /// <summary>
    /// 日志自身日志
    /// </summary>
    public interface ILoggerLog
    {
        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        Task ShutdownAsync();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="consoleColor"></param>
        /// <param name="name"></param>
        void Log(string message, LogLevel logLevel, ConsoleColor consoleColor = ConsoleColor.Gray, string name = "LoggerLog");
        /// <summary>
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        void LogDebug(string message, string name = "LoggerLog");
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        void LogInfomation(string message, string name = "LoggerLog");
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        void LogWarning(string message, string name = "LoggerLog");
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="name"></param>
        void LogWarning(string message, Exception? exception, string name = "LoggerLog");
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        void LogError(string message, string name = "LoggerLog");
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="name"></param>
        void LogError(string message, Exception? exception, string name = "LoggerLog");
    }
}
