namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite日志写入器模型
    /// </summary>
    /// <remarks>
    /// 构造方法
    /// </remarks>
    /// <param name="target"></param>
    /// <param name="model"></param>
    public class SqliteLoggerWriterModel(LoggerWriterModel model, SqliteLoggerTargetConfig target) : BatchLoggerWriterModel(model)
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string Path { get; set; } = LoggerWriterHelper.FormatPath(target.Path, model);
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; } = LoggerWriterHelper.FormatText(target.TableName, model);
        /// <summary>
        /// 字段
        /// </summary>
        public List<SqliteDBFiled> Fileds { get; set; } = target.Fileds.Count <= 0
                ? SqliteLoggerTargetConfig.DefaultFileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList()
                : target.Fileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList();
        /// <summary>
        /// 获得新的Sqlite数据库字段
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private static SqliteDBFiled GetNewSqliteDBFiled(SqliteDBFiled filed, LoggerWriterModel model)
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
                result.Value = LoggerWriterHelper.FormatMessage(filed.Value, model);
            }
            return result;
        }
    }
}
