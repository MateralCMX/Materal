namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// Http日志处理器模型
    /// </summary>
    public class HttpLoggerHandlerModel(LoggerRuleConfigModel rule, HttpLoggerTargetConfigModel target, LoggerHandlerModel model, LoggerConfig loggerConfig) : BufferLoggerHandlerModel(rule, target, model)
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = LoggerHandlerHelper.FormatPath(loggerConfig, target.Url, model);
        /// <summary>
        /// Http方法
        /// </summary>
        public HttpMethod HttpMethod { get; set; } = target.GetHttpMethod();
    }
}
