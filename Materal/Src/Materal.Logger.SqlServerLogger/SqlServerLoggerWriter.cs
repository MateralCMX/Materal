using Materal.Logger.DBLogger;

namespace Materal.Logger.SqlServerLogger
{
    /// <summary>
    /// SqlServer日志写入器
    /// </summary>
    public class SqlServerLoggerWriter(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : DBLoggerWriter<SqlServerLoggerTargetOptions, SqlServerFiled, SqlServerLog, SqlServerRepository>(options, loggerInfo)
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "SqlServer";
    }
}
