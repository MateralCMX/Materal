using Materal.Logger.DBLogger;

namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// MySql日志写入器
    /// </summary>
    public class MySqlLoggerWriter(IOptionsMonitor<LoggerOptions> options, ILoggerInfo loggerInfo) : DBLoggerWriter<MySqlLoggerTargetOptions, MySqlFiled, MySqlLog, MySqlRepository>(options, loggerInfo)
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "MySql";
    }
}
