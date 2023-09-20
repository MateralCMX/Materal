using Microsoft.Extensions.Logging;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志日志等级配置模型
    /// </summary>
    public class LoggerLogLevelConfigModel
    {
        /// <summary>
        /// 最小等级
        /// </summary>
        public LogLevel MinLevel { get; set; } = LogLevel.Warning;
        /// <summary>
        /// 最大等级
        /// </summary>
        public LogLevel MaxLevel { get; set; } = LogLevel.Critical;
    }
}
