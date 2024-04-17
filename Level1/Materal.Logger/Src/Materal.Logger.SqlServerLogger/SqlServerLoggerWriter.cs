using Materal.Logger.DBLogger;

namespace Materal.Logger.SqlServerLogger
{
    /// <summary>
    /// SqlServer日志写入器
    /// </summary>
    public class SqlServerLoggerWriter(IOptionsMonitor<LoggerOptions> options, IHostLogger hostLogger) : DBLoggerWriter<SqlServerLoggerTargetOptions, SqlServerFiled, SqlServerLog, SqlServerRepository>(options, hostLogger)
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "SqlServer";
    }
}
