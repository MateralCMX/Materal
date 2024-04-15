namespace Materal.Logger.SqlServerLogger
{
    /// <summary>
    /// SqlServer日志写入器
    /// </summary>
    public class SqlServerLoggerWriter(SqlServerLoggerTargetConfig targetConfig) : DBLoggerWriter<SqlServerLoggerWriter, SqlServerLoggerWriterModel, SqlServerLoggerTargetConfig, SqlServerDBFiled, SqlServerRepository>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "SqlServer";
    }
}
