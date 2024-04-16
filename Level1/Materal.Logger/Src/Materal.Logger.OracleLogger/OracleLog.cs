using Materal.Logger.BatchLogger;
using Materal.Logger.DBLogger;

namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle日志
    /// </summary>
    public class OracleLog(BatchLog<OracleLoggerTargetOptions> batchLogs, LoggerOptions options) : DBLog<OracleLoggerTargetOptions, OracleFiled>(batchLogs, options)
    {
    }
}
