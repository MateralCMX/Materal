using Materal.Common;
using Materal.Core.Logger.CommandHandlers;
using Materal.Logger.Message;
using Microsoft.Extensions.Logging;

namespace Materal.LoggerClient.EventHandlers
{
    public class LogMessageEventHandler : BaseEventHandler<LogMessageEvent>
    {
        public static List<LogLevel> TargetLogLevels { get; set; } = new();
        public static List<LogLevel> IgnoreLogLevels { get; set; } = new();
        public override void Handler(LogMessageEvent @event)
        {
            if (IgnoreLogLevels.Count > 0 && IgnoreLogLevels.Contains(@event.LogLevel)) return;
            if (TargetLogLevels.Count > 0 && !TargetLogLevels.Contains(@event.LogLevel)) return;
            ConsoleQueue.WriteLine(@event.Message, @event.Color);
        }
    }
}
