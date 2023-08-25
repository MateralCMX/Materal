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
        /// 写Debug
        /// </summary>
        /// <param name="message"></param>
        public static void LogDebug(string message)
        {
            if (LogLevel.Debug < MinLevel || LogLevel.Debug > MaxLevel) return;
            ConsoleQueue.WriteLine($"LoggerLog|{message}", ConsoleColor.DarkGray);
        }
        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        public static void LogInfomation(string message)
        {
            if (LogLevel.Information < MinLevel || LogLevel.Information > MaxLevel) return;
            ConsoleQueue.WriteLine($"LoggerLog|{message}", ConsoleColor.Gray);
        }
        /// <summary>
        /// 写警告
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message)
        {
            if (LogLevel.Warning < MinLevel || LogLevel.Warning > MaxLevel) return;
            ConsoleQueue.WriteLine($"LoggerLog|{message}", ConsoleColor.DarkYellow);
        }
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {
            if (LogLevel.Error < MinLevel || LogLevel.Error > MaxLevel) return;
            StringBuilder messageBuild = new();
            messageBuild.AppendLine($"LoggerLog|{message}");
            ConsoleQueue.WriteLine(messageBuild.ToString(), ConsoleColor.DarkRed);
        }
        /// <summary>
        /// 写错误
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void LogError(string message, Exception? exception)
        {
            if (LogLevel.Error < MinLevel || LogLevel.Error > MaxLevel) return;
            StringBuilder messageBuild= new();
            messageBuild.AppendLine($"LoggerLog|{message}");
            if(exception is not null)
            {
                if(exception is MateralHttpException httpException)
                {
                    messageBuild.AppendLine(httpException.GetExceptionMessage());
                }
                else
                {
                    messageBuild.AppendLine(exception.GetErrorMessage());
                }
            }
            ConsoleQueue.WriteLine(messageBuild.ToString(), ConsoleColor.DarkRed);
        }
    }
}
