using Materal.Logger.BatchLogger;
using Materal.Logger.DBLogger;

namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite日志
    /// </summary>
    /// <param name="batchLogs"></param>
    /// <param name="options"></param>
    public class SqliteLog(BatchLog<SqliteLoggerTargetOptions> batchLogs, LoggerOptions options) : DBLog<SqliteLoggerTargetOptions, SqliteFiled>(batchLogs, options)
    {
    }
}
