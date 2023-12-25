namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// 文件日志处理器模型
    /// </summary>
    public class FileLoggerHandlerModel(LoggerRuleConfigModel rule, FileLoggerTargetConfigModel target, LoggerHandlerModel model, LoggerConfig loggerConfig) : BufferLoggerHandlerModel(rule, target, model)
    {
        /// <summary>
        /// 输出的消息
        /// </summary>
        public string FileContent { get; } = LoggerHandlerHelper.FormatMessage(loggerConfig, target.Format, model);
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; } = LoggerHandlerHelper.FormatPath(loggerConfig, target.Path, model);
        /// <summary>
        /// 颜色
        /// </summary>
        public ConsoleColor Color { get; }
    }
}
