namespace Materal.Logger
{
    /// <summary>
    /// 日志规则配置
    /// </summary>
    public class LoggerRuleOptions
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
        public LogLevel MinLogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Trace;
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevel MaxLogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Critical;
        /// <summary>
        /// 目标组
        /// </summary>
        public List<string> Targets { get; set; } = [];
        /// <summary>
        /// 作用域组
        /// </summary>
        public Dictionary<string, LogLevel>? Scopes { get; set; }
        /// <summary>
        /// 日志等级组
        /// </summary>
        public Dictionary<string, LogLevel>? LogLevel { get; set; }
    }
}
