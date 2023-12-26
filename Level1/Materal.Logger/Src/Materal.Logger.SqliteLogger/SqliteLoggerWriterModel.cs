namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite日志写入器模型
    /// </summary>
    public class SqliteLoggerWriterModel : BatchLoggerWriterModel
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
        /// <param name="target"></param>
        /// <param name="model"></param>
        public SqliteLoggerWriterModel(LoggerWriterModel model, SqliteLoggerTargetConfig target) : base(model)
        {
            Path = LoggerWriterHelper.FormatPath(target.Path, model);
            TableName = LoggerWriterHelper.FormatText(target.TableName, model);
            Fileds = target.Fileds.Count <= 0
                ? SqliteLoggerTargetConfig.DefaultFileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList()
                : target.Fileds.Select(m => GetNewSqliteDBFiled(m, model)).ToList();
        }

        /// <summary>
        /// 获得新的Sqlite数据库字段
        /// </summary>
        /// <param name="filed"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private SqliteDBFiled GetNewSqliteDBFiled(SqliteDBFiled filed, LoggerWriterModel model)
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
