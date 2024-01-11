namespace Materal.Logger.SqlServerLogger
{
    /// <summary>
    /// SqlServer日志写入器模型
    /// </summary>
    public class SqlServerLoggerWriterModel(LoggerWriterModel model, SqlServerLoggerTargetConfig target) : DBLoggerWriterModel<SqlServerLoggerWriter, SqlServerLoggerTargetConfig, SqlServerDBFiled>(model, target)
    {
    }
}
