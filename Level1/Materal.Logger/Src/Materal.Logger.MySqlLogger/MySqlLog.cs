using Materal.Logger.BatchLogger;
using Materal.Logger.DBLogger;

namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// MySql日志
    /// </summary>
    public class MySqlLog(BatchLog<MySqlLoggerTargetOptions> batchLogs, LoggerOptions options) : DBLog<MySqlLoggerTargetOptions, MySqlFiled>(batchLogs, options)
    {
    }
}
