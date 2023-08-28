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
        public string? Data { get; set; }
        /// <summary>
        /// Http日志处理器模型
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="target"></param>
        /// <param name="model"></param>
        public HttpLoggerHandlerModel(LoggerRuleConfigModel rule, LoggerTargetConfigModel target, LoggerHandlerModel model) : base(rule, target, model)
        {
            if (target.Url is null || string.IsNullOrWhiteSpace(target.Url))
            {
                IsOK = false;
                return;
            }
            Url = target.Url;
            HttpMethod = target.HttpMethod switch
            {
                "Get" => HttpMethod.Get,
                "Post" => HttpMethod.Post,
                "Put" => HttpMethod.Put,
                "Delete" => HttpMethod.Delete,
                _ => HttpMethod.Post
            };
            string createTimeText = model.CreateTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffZ");
            Data = Regex.Replace(target.Format, @"\$\{DateTime\}", createTimeText);
            Data = LoggerHandlerHelper.FormatMessage(Data, model.LogLevel, model.Message, model.CategoryName, model.Scope, model.CreateTime, model.Exception, model.ThreadID);
        }
    }
}
