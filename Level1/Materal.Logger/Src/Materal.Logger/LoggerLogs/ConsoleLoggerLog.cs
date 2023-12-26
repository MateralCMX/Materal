using Materal.Utils.Http;

namespace Materal.Logger.LoggerLogs
{
    /// <summary>
    /// 控制台日志自身日志
    /// </summary>
    public class ConsoleLoggerLog : ILoggerLog
    {
        /// <summary>
        /// 最小等级
        /// </summary>
        public LogLevel MinLevel { get; set; } = LogLevel.Trace;
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevel MaxLevel { get; set; } = LogLevel.Critical;
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public async Task ShutdownAsync() => await ConsoleQueue.ShutdownAsync();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="consoleColor"></param>
        /// <param name="name"></param>
        public void Log(string message, LogLevel logLevel, ConsoleColor consoleColor = ConsoleColor.Gray, string name = "Loggerlog")
        {
            if (logLevel < MinLevel || logLevel > MaxLevel) return;
            ConsoleQueue.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{name}|{logLevel}|{message}", consoleColor);
        }
        /// <summary>
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        public void LogDebug(string message, string name = "Loggerlog") => Log(message, LogLevel.Debug, ConsoleColor.DarkGreen, name);
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        public void LogInfomation(string message, string name = "Loggerlog") => Log(message, LogLevel.Information, ConsoleColor.Gray, name);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        public void LogWarning(string message, string name = "Loggerlog") => Log(message, LogLevel.Warning, ConsoleColor.DarkYellow, name);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="name"></param>
        public void LogWarning(string message, Exception? exception, string name = "Loggerlog") => LogWarning(GetErrorMessage(message, exception), name);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="name"></param>
        public void LogError(string message, string name = "Loggerlog") => Log(message, LogLevel.Error, ConsoleColor.DarkRed, name);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="name"></param>
        public void LogError(string message, Exception? exception, string name = "Loggerlog") => LogError(GetErrorMessage(message, exception), name);
        /// <summary>
        /// 获得错误消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static string GetErrorMessage(string message, Exception? exception)
        {
            StringBuilder messageBuild = new();
            messageBuild.AppendLine(message);
            if (exception is not null)
            {
                switch (exception)
                {
                    case MateralHttpException httpException:
                        messageBuild.AppendLine(httpException.GetExceptionMessage());
                        break;
                    default:
                        messageBuild.AppendLine(exception.GetErrorMessage());
                        break;
                }
            }
            return messageBuild.ToString();
        }
    }
}
