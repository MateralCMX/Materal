namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite日志写入器模型
    /// </summary>
    public class SqliteLoggerWriterModel(LoggerWriterModel model, SqliteLoggerTargetConfig target) : DBLoggerWriterModel<SqliteLoggerWriter, SqliteLoggerTargetConfig, SqliteDBFiled>(model, target)
    {
        /// <summary>
        /// 数据库路径
        /// </summary>
        public string Path { get; set; } = LoggerWriterHelper.FormatPath(target.Path, model);
    }
}
