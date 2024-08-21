using Materal.Logger.DBLogger;

namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle日志写入器
    /// </summary>
    public class OracleLoggerWriter(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : DBLoggerWriter<OracleLoggerTargetOptions, OracleFiled, OracleLog, OracleRepository>(options, loggerInfo)
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "Oracle";
    }
}
