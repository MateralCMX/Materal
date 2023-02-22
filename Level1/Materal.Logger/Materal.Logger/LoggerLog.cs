using Materal.Common;
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
        public static LogLevel MinLevel { get; set; } = LogLevel.Warning;
        /// <summary>
        /// 最大等级
        /// </summary>
        public static LogLevel MaxLevel { get; set; } = LogLevel.Critical;
        /// <summary>
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        public static void LogDebug(string message)
        {
            if (LogLevel.Debug < MinLevel || LogLevel.Debug > MaxLevel) return;
            ConsoleQueue.WriteLine(message, ConsoleColor.DarkGray);
        }
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        public static void LogInfomation(string message)
        {
            if (LogLevel.Debug < MinLevel || LogLevel.Debug > MaxLevel) return;
            ConsoleQueue.WriteLine(message, ConsoleColor.Gray);
        }
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message)
        {
            if (LogLevel.Debug < MinLevel || LogLevel.Debug > MaxLevel) return;
            ConsoleQueue.WriteLine(message, ConsoleColor.DarkYellow);
        }
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void LogError(string message, Exception exception)
        {
            if (LogLevel.Debug < MinLevel || LogLevel.Debug > MaxLevel) return;
            StringBuilder messageBuild= new();
            messageBuild.AppendLine(message);
            messageBuild.AppendLine(exception.GetErrorMessage());
            ConsoleQueue.WriteLine(messageBuild.ToString(), ConsoleColor.DarkRed);
        }
    }
}
