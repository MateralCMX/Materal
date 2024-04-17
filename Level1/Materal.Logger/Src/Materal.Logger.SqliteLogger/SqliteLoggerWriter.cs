using Materal.Logger.DBLogger;

namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite日志写入器
    /// </summary>
    public class SqliteLoggerWriter(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : DBLoggerWriter<SqliteLoggerTargetOptions, SqliteFiled, SqliteLog, SqliteRepository>(options, loggerInfo)
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "Sqlite";
    }
}
