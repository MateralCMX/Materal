namespace Materal.Logger
{
    /// <summary>
    /// 日志
    /// </summary>
    public partial class Log
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// 日志等级
        /// </summary>
        public LogLevel Level { get; set; }
        /// <summary>
        /// 事件ID
        /// </summary>
        public EventId EventID { get; set; }
        /// <summary>
        /// 线程ID
        /// </summary>
        public int ThreadID { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 异常
        /// </summary>
        public Exception? Exception { get; set; }
        /// <summary>
        /// 作用域提供者
        /// </summary>
        public string ScopeName { get; set; }
        /// <summary>
        /// 域数据
        /// </summary>
        public Dictionary<string, object?> ScopeData { get; } = [];
    }
}
