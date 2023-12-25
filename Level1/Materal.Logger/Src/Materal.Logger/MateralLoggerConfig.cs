using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// MateralLogger配置
    /// </summary>
    public class MateralLoggerConfig
    {
        /// <summary>
        /// 最小日志等级
        /// </summary>
        public LogLevel MinLogLevel { get; set; } = LogLevel.Trace;
        /// <summary>
        /// 最大日志等级
        /// </summary>
        public LogLevel MaxLogLevel { get; set; } = LogLevel.Critical;
    }
}
