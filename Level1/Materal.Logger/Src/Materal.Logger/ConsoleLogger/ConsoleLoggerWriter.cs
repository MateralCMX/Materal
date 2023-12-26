namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 控制台日志写入器
    /// </summary>
    public class ConsoleLoggerWriter(ConsoleLoggerTargetConfig targetConfig) : BaseLoggerWriter<ConsoleLoggerWriterModel, ConsoleLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task WriteLoggerAsync(ConsoleLoggerWriterModel model)
        {
            ConsoleQueue.WriteLine(model.WriteContent, model.WriteColor);
            await Task.CompletedTask;
        }
    }
}
