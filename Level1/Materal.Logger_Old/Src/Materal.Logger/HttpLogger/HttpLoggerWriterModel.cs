namespace Materal.Logger.HttpLogger
{
    /// <summary>
    /// Http日志写入器模型
    /// </summary>
    public class HttpLoggerWriterModel(LoggerWriterModel model, HttpLoggerTargetConfig target) : BatchLoggerWriterModel(model)
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = LoggerWriterHelper.FormatPath(target.Url, model);
        /// <summary>
        /// Http方法
        /// </summary>
        public HttpMethod HttpMethod { get; set; } = target.GetHttpMethod();
    }
}
