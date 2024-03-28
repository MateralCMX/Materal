namespace Materal.Logger
{
    /// <summary>
    /// 日志模型
    /// </summary>
    public class LogModel(string? categoryName, LogLevel logLevel, string message, Exception? exception = null, LoggerScope? scope = null)
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; } = logLevel;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 异常
        /// </summary>
        public Exception? Exception { get; set; } = exception;
        /// <summary>
        /// 线程ID
        /// </summary>
        public string ThreadID { get; set; } = Environment.CurrentManagedThreadId.ToString();
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = message;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string? CategoryName { get; set; } = categoryName;
        /// <summary>
        /// 作用域
        /// </summary>
        public LoggerScope Scope { get; set; } = scope ?? new LoggerScope("PublicScope");
    }
}
