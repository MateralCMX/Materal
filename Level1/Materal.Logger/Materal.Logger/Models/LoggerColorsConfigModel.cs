using Microsoft.Extensions.Logging;

namespace Materal.Logger.Models
{
    /// <summary>
    /// 日志颜色配置模型
    /// </summary>
    public class LoggerColorsConfigModel
    {
        /// <summary>
        /// 默认颜色
        /// </summary>
        public static ConsoleColor Default => ConsoleColor.Gray;
        /// <summary>
        /// Trace颜色
        /// </summary>
        public ConsoleColor Trace { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// Debug颜色
        /// </summary>
        public ConsoleColor Debug { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// Information颜色
        /// </summary>
        public ConsoleColor Information { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// Warning颜色
        /// </summary>
        public ConsoleColor Warning { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// Error颜色
        /// </summary>
        public ConsoleColor Error { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// Critical颜色
        /// </summary>
        public ConsoleColor Critical { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// None颜色
        /// </summary>
        public ConsoleColor None { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// 获得控制台颜色
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public ConsoleColor GetConsoleColor(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => Trace,
                LogLevel.Debug => Debug,
                LogLevel.Information => Information,
                LogLevel.Warning => Warning,
                LogLevel.Error => Error,
                LogLevel.Critical => Critical,
                LogLevel.None => None,
                _ => Default
            };
        }
    }
}
