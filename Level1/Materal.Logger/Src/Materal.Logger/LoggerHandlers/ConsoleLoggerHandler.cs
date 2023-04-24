using Materal.Logger.Models;
using Materal.Utils;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.LoggerHandlers
{
    /// <summary>
    /// 控制台日志处理器
    /// </summary>
    public class ConsoleLoggerHandler : LoggerHandler
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        public ConsoleLoggerHandler(LoggerRuleConfigModel rule, LoggerTargetConfigModel target) : base(rule, target)
        {
        }
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="message"></param>
        /// <param name="categoryName"></param>
        /// <param name="scope"></param>
        /// <param name="dateTime"></param>
        /// <param name="exception"></param>
        /// <param name="threadID"></param>
        public override void Handler(LogLevel logLevel, string message, string? categoryName, LoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            string writeMessage = FormatMessage(Target.Format, logLevel, message, categoryName, scope, dateTime, exception, threadID);
            ConsoleColor color = Target.Colors.GetConsoleColor(logLevel);
            ConsoleQueue.WriteLine(writeMessage, color);
            SendMessage(writeMessage, logLevel);
        }
    }
}
