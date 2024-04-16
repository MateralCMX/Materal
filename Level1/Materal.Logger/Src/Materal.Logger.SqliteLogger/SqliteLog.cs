using Materal.Logger.BatchLogger;
using Materal.Logger.DBLogger;

namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite日志
    /// </summary>
    public class SqliteLog(BatchLog<SqliteLoggerTargetOptions> batchLogs, LoggerOptions options) : DBLog<SqliteLoggerTargetOptions, SqliteFiled>(batchLogs, options)
    {
    }
}
