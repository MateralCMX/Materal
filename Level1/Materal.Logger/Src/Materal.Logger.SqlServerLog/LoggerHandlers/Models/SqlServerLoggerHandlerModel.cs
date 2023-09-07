using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// SqlServer日志处理器模型
    /// </summary>
    public class SqlServerLoggerHandlerModel : BufferLoggerHandlerModel
    {
        /// <summary>
        /// 日志模型
        /// </summary>
        public LogModel LogModel { get; set; } = new();
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public SqlServerLoggerHandlerModel(LoggerRuleConfigModel rule, SqlServerLoggerTargetConfigModel target, LoggerHandlerModel model) : base(rule, target, model)
        {
            LogModel = LoggerHandlerHelper.GetLogModel(model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID);
            ConnectionString = LoggerHandlerHelper.FormatPath(target.ConnectionString, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
        }
    }
}
