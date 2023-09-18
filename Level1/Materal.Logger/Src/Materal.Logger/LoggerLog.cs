using Materal.Utils;
using Materal.Utils.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Materal.Logger
{
    /// <summary>
    /// 日志对象日志
    /// </summary>
    public static class LoggerLog
    {
        /// <summary>
        /// 最小等级
        /// </summary>
        public static LogLevel MinLevel => LoggerConfig.LogLogLevel.MinLevel;
        /// <summary>
        /// 最大等级
        /// </summary>
        public static LogLevel MaxLevel => LoggerConfig.LogLogLevel.MaxLevel;
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logLevel"></param>
        /// <param name="consoleColor"></param>
        public static void Log(string message, LogLevel logLevel, ConsoleColor consoleColor = ConsoleColor.Gray)
        {
            if (logLevel < MinLevel || logLevel > MaxLevel) return;
            ConsoleQueue.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}|LoggerLog|{logLevel}|{message}", consoleColor);
        }
        /// <summary>
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        public static void LogDebug(string message) => Log(message, LogLevel.Debug, ConsoleColor.DarkGreen);
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        public static void LogInfomation(string message) => Log(message, LogLevel.Information, ConsoleColor.Gray);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message) => Log(message, LogLevel.Warning, ConsoleColor.DarkYellow);
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void LogWarning(string message, Exception? exception) => LogWarning(GetErrorMessage(message, exception));
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message) => Log(message, LogLevel.Error, ConsoleColor.DarkRed);
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void LogError(string message, Exception? exception) => LogError(GetErrorMessage(message, exception));
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
            if(exception is not null)
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
