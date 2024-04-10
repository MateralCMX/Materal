namespace Materal.Logger
{
    /// <summary>
    /// 日志消息模型
    /// </summary>
    public partial class LogMessageModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public LogLevel Level { get; set; }
        /// <summary>
        /// 进程ID
        /// </summary>
        public string ProgressID { get; set; } = string.Empty;
        /// <summary>
        /// 线程ID
        /// </summary>
        public string ThreadID { get; set; } = string.Empty;
        /// <summary>
        /// 域
        /// </summary>
        public string? Scope { get; set; }
        /// <summary>
        /// 机器名称
        /// </summary>
        public string MachineName { get; set; } = string.Empty;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string? CategoryName { get; set; }
        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; set; } = string.Empty;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// 异常对象
        /// </summary>
        public string? Exception { get; set; }
        /// <summary>
        /// 域数据
        /// </summary>
        public Dictionary<string, object?>? ScopeData { get; set; }
    }
}
