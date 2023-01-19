using Materal.Common;
using Materal.Logger.Models;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.LoggerHandlers
{
    public class ConsoleLoggerHandler : LoggerHandler
    {
        public ConsoleLoggerHandler(MateralLoggerRuleConfigModel rule, MateralLoggerTargetConfigModel target) : base(rule, target)
        {
        }

        public override void Handler(LogLevel logLevel, string message, string? categoryName, MateralLoggerScope? scope, DateTime dateTime, Exception? exception, string threadID)
        {
            string writeMessage = FormatMessage(Target.Format, logLevel, message, categoryName, scope, dateTime, exception, threadID);
            ConsoleColor color = Target.Colors.GetConsoleColor(logLevel);
            ConsoleQueue.WriteLine(writeMessage, color);
            SendMessage(writeMessage, logLevel);
        }
    }
}
