using Materal.Logger.LoggerWriter;

namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 批量控制台日志写入器模型
    /// </summary>
    /// <param name="model"></param>
    public class BatchConsoleLoggerWriterModel(LoggerWriterModel model) : BatchLoggerWriterModel(model)
    {
    }
}
