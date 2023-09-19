using Materal.Logger.LoggerHandlers;
using Materal.Logger.LoggerHandlers.Models;
using Microsoft.Extensions.Logging;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志消息模型
    /// </summary>
    public class LogMessageModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; }
        /// <summary>
        /// 级别
        /// </summary>
        public LogLevel Level { get; }
        /// <summary>
        /// 进程ID
        /// </summary>
        public string ProgressID { get; }
        /// <summary>
        /// 线程ID
        /// </summary>
        public string ThreadID { get; }
        /// <summary>
        /// 域
        /// </summary>
        public string? Scope { get; }
        /// <summary>
        /// 机器名称
        /// </summary>
        public string MachineName { get; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string? CategoryName { get; }
        /// <summary>
        /// 应用程序
        /// </summary>
        public string Application { get; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; }
        /// <summary>
        /// 异常对象
        /// </summary>
        public string? Exception { get; }
        /// <summary>
        /// 域数据
        /// </summary>
        public Dictionary<string, string>? ScopeData { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="model"></param>
        /// <param name="loggerConfig"></param>
        public LogMessageModel(LoggerHandlerModel model, LoggerConfig loggerConfig)
        {
            ID = model.ID;
            CreateTime = model.CreateTime;
            Level = model.LogLevel;
            ProgressID = LoggerHandlerHelper.GetProgressID();
            ThreadID = model.ThreadID;
            Scope = model.Scope?.ScopeName;
            MachineName = LoggerHandlerHelper.MachineName;
            CategoryName = model.CategoryName;
            Application = loggerConfig.Application;
            Message = LoggerHandlerHelper.FormatText(loggerConfig, model.Message, model);
            Exception = model.Exception?.GetErrorMessage();
            ScopeData = model.Scope?.AdvancedScope?.ScopeData;
        }
    }
}
