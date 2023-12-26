using Materal.Logger.LoggerWriter;

namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 批量控制台日志写入器
    /// </summary>
    public class BatchConsoleLoggerWriter(BatchConsoleLoggerTargetConfig targetConfig) : BatchLoggerWriter<BatchConsoleLoggerWriterModel, BatchConsoleLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写批量日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(BatchConsoleLoggerWriterModel[] model)
        {
            foreach (var item in model)
            {
                Console.WriteLine(item.Message);
            }
            await Task.CompletedTask;
        }
    }
}
