namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle日志写入器
    /// </summary>
    public class OracleLoggerWriter(OracleLoggerTargetConfig targetConfig) : DBLoggerWriter<OracleLoggerWriter, OracleLoggerWriterModel, OracleLoggerTargetConfig, OracleDBFiled, OracleRepository>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public override string DBName => "Oracle";
    }
}
