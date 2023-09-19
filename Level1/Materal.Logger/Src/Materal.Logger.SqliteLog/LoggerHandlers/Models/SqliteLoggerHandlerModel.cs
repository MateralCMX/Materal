using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// Sqlite日志处理器模型
    /// </summary>
    public class SqliteLoggerHandlerModel : BufferLoggerHandlerModel
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public List<SqliteDBFiled> Fileds { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        /// <param name="loggerConfig"></param>
        public SqliteLoggerHandlerModel(LoggerRuleConfigModel rule, SqliteLoggerTargetConfigModel target, LoggerHandlerModel model, LoggerConfig loggerConfig) : base(rule, target, model)
        {
            Path = LoggerHandlerHelper.FormatPath(loggerConfig, target.Path, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
            TableName = LoggerHandlerHelper.FormatText(loggerConfig, target.TableName, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
            Fileds = target.Fileds.Count <= 0
                ? SqliteLoggerTargetConfigModel.DefaultFileds.Select(m => GetNewSqliteDBFiled(m, model, loggerConfig)).ToList()
                : target.Fileds.Select(m => GetNewSqliteDBFiled(m, model, loggerConfig)).ToList();
        }
        /// <summary>
        /// 获得新的Sqlite数据库字段
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="model"></param>
        /// <param name="loggerConfig"></param>
        /// <returns></returns>
        private SqliteDBFiled GetNewSqliteDBFiled(SqliteDBFiled filed, LoggerHandlerModel model, LoggerConfig loggerConfig)
        {
            SqliteDBFiled result = new()
            {
                Name = filed.Name,
                Type = filed.Type,
                PK = filed.PK,
                Index = filed.Index,
                IsNull = filed.IsNull
            };
            if (filed.Value is not null && !string.IsNullOrWhiteSpace(filed.Value))
            {
                result.Value = LoggerHandlerHelper.FormatMessage(loggerConfig, filed.Value, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID, model.ID);
            }
            return result;
        }
    }
}
