using Newtonsoft.Json;
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
        /// 名称
        /// </summary>
        public string Name { get; set; } = "规则";
        /// <summary>
        /// 最小等级
        /// </summary>
        public LogLevel MinLevel { get; set; } = LogLevel.Trace;
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevel MaxLevel { get; set; } = LogLevel.Critical;
        /// <summary>
        /// 目标组
        /// </summary>
        public List<string> Targets { get; set; } = [];
        /// <summary>
        /// 域组
        /// </summary>
        public List<string>? Scopes { get; set; }
        /// <summary>
        /// 日志等级组
        /// </summary>
        [JsonPropertyName("LogLevel")]
        [JsonProperty(PropertyName = "LogLevel")]
        public Dictionary<string, LogLevel>? LogLevels { get; set; }
    }
}
