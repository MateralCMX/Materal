using Materal.Logger.DBLogger;

namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle日志写入器
    /// </summary>
    public class OracleLoggerWriter(IOptionsMonitor<LoggerOptions> options, IHostLogger hostLogger) : DBLoggerWriter<OracleLoggerTargetOptions, OracleFiled, OracleLog, OracleRepository>(options, hostLogger)
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "Oracle";
    }
}
