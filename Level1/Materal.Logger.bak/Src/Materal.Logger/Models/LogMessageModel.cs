namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志消息模型
    /// </summary>
    public class LogMessageModel(LoggerHandlerModel model, LoggerConfig loggerConfig)
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; } = model.ID;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; } = model.CreateTime;
        /// <summary>
        /// 级别
        /// </summary>
        public LogLevel Level { get; } = model.LogLevel;
        /// <summary>
        /// 进程ID
        /// </summary>
        public string ProgressID { get; } = LoggerHandlerHelper.GetProgressID();
        /// <summary>
        /// 线程ID
        /// </summary>
        public string ThreadID { get; } = model.ThreadID;
        /// <summary>
        /// 域
        /// </summary>
        public string? Scope { get; } = model.Scope?.ScopeName;
        /// <summary>
        /// 机器名称
        /// </summary>
        public string MachineName { get; } = LoggerHandlerHelper.MachineName;
        /// <summary>
        /// 类型名称
        /// </summary>
        public string? CategoryName { get; } = model.CategoryName;
        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; } = loggerConfig.Application;
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; } = LoggerHandlerHelper.FormatText(loggerConfig, model.Message, model);
        /// <summary>
        /// 异常对象
        /// </summary>
        public string? Exception { get; } = model.Exception?.GetErrorMessage();
        /// <summary>
        /// 域数据
        /// </summary>
        public Dictionary<string, object?>? ScopeData { get; } = model.Scope?.AdvancedScope?.ScopeData;
    }
}
