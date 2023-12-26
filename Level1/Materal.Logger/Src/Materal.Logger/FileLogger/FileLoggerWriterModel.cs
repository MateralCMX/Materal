namespace Materal.Logger.FileLogger
{
    /// <summary>
    /// 控制台日志写入器模型
    /// </summary>
    public class FileLoggerWriterModel(LoggerWriterModel model, FileLoggerTargetConfig target) : BatchLoggerWriterModel(model)
    {
        /// <summary>
        /// 输出的消息
        /// </summary>
        public string FileContent { get; } = LoggerWriterHelper.FormatMessage(target.Format, model);
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; } = LoggerWriterHelper.FormatPath(target.Path, model);
    }
}
