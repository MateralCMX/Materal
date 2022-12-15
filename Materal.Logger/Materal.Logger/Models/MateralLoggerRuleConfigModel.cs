using Microsoft.Extensions.Logging;

namespace Materal.Logger.Models
{
    public class MateralLoggerRuleConfigModel
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; }
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
        public List<string> Targets { get; set; } = new();
        /// <summary>
        /// 忽略组
        /// </summary>
        public List<string> Ignores { get; set; } = new();
    }
}
