using LogLevelEnum = Microsoft.Extensions.Logging.LogLevel;

namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// 日志规则配置模型
    /// </summary>
    public class RuleConfig
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
        public LogLevelEnum MinLogLevel { get; set; } = LogLevelEnum.Trace;
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevelEnum MaxLogLevel { get; set; } = LogLevelEnum.Critical;
        /// <summary>
        /// 目标组
        /// </summary>
        public List<string> Targets { get; set; } = [];
        /// <summary>
        /// 域组
        /// </summary>
        public List<string> Scopes { get; set; } = [];
        /// <summary>
        /// 日志等级组
        /// </summary>
        public Dictionary<string, LogLevelEnum>? LogLevel { get; set; }
    }
}
