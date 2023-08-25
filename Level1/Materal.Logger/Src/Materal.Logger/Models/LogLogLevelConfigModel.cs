using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志日志等级配置模型
    /// </summary>
    public class LogLogLevelConfigModel
    {
        /// <summary>
        /// 最小等级
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LogLevel MinLevel { get; set; } = LogLevel.Trace;
        /// <summary>
        /// 最大等级
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LogLevel MaxLevel { get; set; } = LogLevel.Critical;
    }
}
