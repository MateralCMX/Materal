namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// Oracle日志写入器模型
    /// </summary>
    public class OracleLoggerWriterModel(LoggerWriterModel model, OracleLoggerTargetConfig target) : DBLoggerWriterModel<OracleLoggerWriter, OracleLoggerTargetConfig, OracleDBFiled>(model, target)
    {
    }
}
