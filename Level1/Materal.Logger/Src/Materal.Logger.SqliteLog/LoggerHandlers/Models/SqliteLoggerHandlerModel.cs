using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// Sqlite日志处理器模型
    /// </summary>
    public class SqliteLoggerHandlerModel : BufferLoggerHandlerModel
    {
        /// <summary>
        /// 日志模型
        /// </summary>
        public LogModel LogModel { get; set; } = new();
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string Path { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public SqliteLoggerHandlerModel(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model) : base(rule, target, model)
        {
            if (target.ConnectionString is not null && !string.IsNullOrWhiteSpace(target.ConnectionString))
            {
                Path = LoggerHandlerHelper.FormatPath(target.ConnectionString, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
            }
            else if (target.Path is not null && !string.IsNullOrWhiteSpace(target.Path))
            {
                Path = LoggerHandlerHelper.FormatPath(target.Path, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
            }
            else
            {
                IsOK = false;
                return;
            }
            LogModel = LoggerHandlerHelper.GetMateralLogModel(model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID);
        }
    }
}
