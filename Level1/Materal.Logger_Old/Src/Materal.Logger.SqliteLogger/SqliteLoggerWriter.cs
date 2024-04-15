namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite日志写入器
    /// </summary>
    public class SqliteLoggerWriter(SqliteLoggerTargetConfig targetConfig) : DBLoggerWriter<SqliteLoggerWriter, SqliteLoggerWriterModel, SqliteLoggerTargetConfig, SqliteDBFiled, SqliteRepository>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "Sqlite";
    }
}
