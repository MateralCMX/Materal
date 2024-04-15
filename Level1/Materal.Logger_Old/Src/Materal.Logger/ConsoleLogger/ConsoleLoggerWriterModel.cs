namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 控制台日志写入器模型
    /// </summary>
    public class ConsoleLoggerWriterModel(LoggerWriterModel model, ConsoleLoggerTargetConfig targetConfig) : LoggerWriterModel(model)
    {
        /// <summary>
        /// 写入内容
        /// </summary>
        public string WriteContent { get; set; } = LoggerWriterHelper.FormatMessage(targetConfig.Format, model);
        /// <summary>
        /// 写入颜色
        /// </summary>
        public ConsoleColor WriteColor { get; set; } = targetConfig.Colors.GetConsoleColor(model.LogLevel);
    }
}
