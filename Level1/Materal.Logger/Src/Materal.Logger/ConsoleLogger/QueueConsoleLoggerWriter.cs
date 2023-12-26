using Materal.Logger.LoggerWriter;

namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 队列控制台日志写入器
    /// </summary>
    public class QueueConsoleLoggerWriter(QueueConsoleLoggerTargetConfig targetConfig) : QueueLoggerWriter<QueueConsoleLoggerWriterModel, QueueConsoleLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写队列日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override async Task WriteQueueLoggerAsync(QueueConsoleLoggerWriterModel model)
        {
            Console.WriteLine(model.Message);
            await Task.CompletedTask;
        }
    }
}
