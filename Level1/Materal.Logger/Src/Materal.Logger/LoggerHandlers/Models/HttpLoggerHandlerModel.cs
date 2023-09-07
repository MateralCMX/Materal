using Materal.Logger.Models;
using System.Text.RegularExpressions;

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
        /// 数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// Http日志处理器模型
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public HttpLoggerHandlerModel(LoggerRuleConfigModel rule, HttpLoggerTargetConfigModel target, LoggerHandlerModel model) : base(rule, target, model)
        {
            Url = target.Url;
            HttpMethod = target.GetHttpMethod();
            string createTimeText = model.CreateTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffZ");
            Data = Regex.Replace(target.Format, @"\$\{DateTime\}", createTimeText);
            Data = LoggerHandlerHelper.FormatMessage(Data, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID);
        }
    }
}
