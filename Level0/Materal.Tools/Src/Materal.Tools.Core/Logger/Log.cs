using Microsoft.Extensions.Logging;

namespace Materal.Tools.Core.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public partial class Log(LogLevel level, EventId eventID, string categoryName, string message, Exception? exception)
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel Level { get; set; } = level;
        /// <summary>
        /// 事件ID
        /// </summary>
        public EventId EventID { get; set; } = eventID;
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; } = categoryName;
        /// <summary>
        /// 状态
        /// </summary>
        public string Message { get; set; } = message;
        /// <summary>
        /// 异常
        /// </summary>
        public Exception? Exception { get; set; } = exception;
    }
}
