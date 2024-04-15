namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// MySql日志写入器
    /// </summary>
    public class MySqlLoggerWriter(MySqlLoggerTargetConfig targetConfig) : DBLoggerWriter<MySqlLoggerWriter, MySqlLoggerWriterModel, MySqlLoggerTargetConfig, MySqlDBFiled, MySqlRepository>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "MySql";
    }
}
