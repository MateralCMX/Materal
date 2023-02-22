using Microsoft.Extensions.Logging;

namespace Materal.Logger.Message
{
    /// <summary>
    /// 日志消息事件
    /// </summary>
    public class LogMessageEvent : BaseEvent
    {
        /// <summary>
        /// 控制台显示颜色
        /// </summary>
        public ConsoleColor Color { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
