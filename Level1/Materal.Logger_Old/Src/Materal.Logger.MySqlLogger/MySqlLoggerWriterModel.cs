namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// MySql日志写入器模型
    /// </summary>
    public class MySqlLoggerWriterModel(LoggerWriterModel model, MySqlLoggerTargetConfig target) : DBLoggerWriterModel<MySqlLoggerWriter, MySqlLoggerTargetConfig, MySqlDBFiled>(model, target)
    {
    }
}
