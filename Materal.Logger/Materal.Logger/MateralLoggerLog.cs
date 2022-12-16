using Materal.Common;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Materal.Logger
{
    public static class MateralLoggerLog
    {
        public static LogLevel MinLevel { get; set; } = LogLevel.Warning;
        public static LogLevel MaxLevel { get; set; } = LogLevel.Critical;
        public static void LogDebug(string message)
        {
            if (LogLevel.Debug < MinLevel || LogLevel.Debug > MaxLevel) return;
            ConsoleQueue.WriteLine(message, ConsoleColor.DarkGray);
        }
        public static void LogInfomation(string message)
        {
            if (LogLevel.Debug < MinLevel || LogLevel.Debug > MaxLevel) return;
            ConsoleQueue.WriteLine(message, ConsoleColor.Gray);
        }
        public static void LogWarning(string message)
        {
            if (LogLevel.Debug < MinLevel || LogLevel.Debug > MaxLevel) return;
            ConsoleQueue.WriteLine(message, ConsoleColor.DarkYellow);
        }
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
