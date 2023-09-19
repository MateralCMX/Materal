using Materal.Logger.Models;

namespace Materal.Logger.LoggerHandlers.Models
{
    /// <summary>
    /// Http日志处理器模型
    /// </summary>
    public class HttpLoggerHandlerModel : BufferLoggerHandlerModel
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// Http方法
        /// </summary>
        public HttpMethod HttpMethod { get; set; } = HttpMethod.Post;
        /// <summary>
        /// 构造方法
        /// </summary>
        public HttpLoggerHandlerModel(LoggerRuleConfigModel rule, HttpLoggerTargetConfigModel target, LoggerHandlerModel model, LoggerConfig loggerConfig) : base(rule, target, model)
        {
            Url = LoggerHandlerHelper.FormatPath(loggerConfig, target.Url, model);
            HttpMethod = target.GetHttpMethod();
        }
    }
}
