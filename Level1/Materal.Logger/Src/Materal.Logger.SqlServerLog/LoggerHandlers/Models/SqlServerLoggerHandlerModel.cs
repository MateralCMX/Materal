using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// SqlServer日志处理器模型
    /// </summary>
    public class SqlServerLoggerHandlerModel : BufferLoggerHandlerModel
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 字段
        /// </summary>
        public List<SqlServerDBFiled> Fileds { get; set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public SqlServerLoggerHandlerModel(LoggerRuleConfigModel rule, SqlServerLoggerTargetConfigModel target, LoggerHandlerModel model) : base(rule, target, model)
        {
            ConnectionString = LoggerHandlerHelper.FormatPath(target.ConnectionString, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
            TableName = LoggerHandlerHelper.FormatText(target.TableName, model.LogLevel, model.CategoryName, model.Scope, model.CreateTime, model.ThreadID);
            Fileds = target.Fileds.Count <= 0
                ? SqlServerLoggerTargetConfigModel.DefaultFileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList()
                : target.Fileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList();
        }
        /// <summary>
        /// 获得新的Sqlite数据库字段
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private SqlServerDBFiled GetNewSqliteDBFiled(SqlServerDBFiled filed, LoggerHandlerModel model)
        {
            SqlServerDBFiled result = new()
            {
                Name = filed.Name,
                Type = filed.Type,
                PK = filed.PK,
                Index = filed.Index,
                IsNull = filed.IsNull
            };
            if (filed.Value is not null && !string.IsNullOrWhiteSpace(filed.Value))
            {
                result.Value = LoggerHandlerHelper.FormatMessage(filed.Value, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID, model.ID);
            }
            return result;
        }
    }
}
