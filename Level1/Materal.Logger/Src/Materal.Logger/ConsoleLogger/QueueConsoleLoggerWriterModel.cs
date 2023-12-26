using Materal.Logger.LoggerWriter;

namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 队列控制台日志写入器模型
    /// </summary>
    /// <param name="model"></param>
    public class QueueConsoleLoggerWriterModel(LoggerWriterModel model) : QueueLoggerWriterModel(model)
    {
    }
}
