using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志规则配置模型
    /// </summary>
    public class LoggerRuleConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 最小等级
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LogLevel MinLevel { get; set; } = LogLevel.Debug;
        /// <summary>
        /// 最大等级
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LogLevel MaxLevel { get; set; } = LogLevel.Critical;
        /// <summary>
        /// 目标组
        /// </summary>
        public List<string> Targets { get; set; } = new();
        /// <summary>
        /// 忽略组
        /// </summary>
        public List<string> Ignores { get; set; } = new();
    }
}
