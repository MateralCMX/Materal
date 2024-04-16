using Materal.Logger.BatchLogger;
using Materal.Logger.DBLogger;

namespace Materal.Logger.SqlServerLogger
{
    /// <summary>
    /// SqlServer日志
    /// </summary>
    /// <param name="batchLogs"></param>
    /// <param name="options"></param>
    public class SqlServerLog(BatchLog<SqlServerLoggerTargetOptions> batchLogs, LoggerOptions options) : DBLog<SqlServerLoggerTargetOptions, SqlServerFiled>(batchLogs, options)
    {
    }
}
