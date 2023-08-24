using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;

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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsoleColor Trace { get; set; } = ConsoleColor.DarkGray;
        /// <summary>
        /// Debug颜色
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsoleColor Debug { get; set; } = ConsoleColor.DarkGreen;
        /// <summary>
        /// Information颜色
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsoleColor Information { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// Warning颜色
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsoleColor Warning { get; set; } = ConsoleColor.DarkYellow;
        /// <summary>
        /// Error颜色
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsoleColor Error { get; set; } = ConsoleColor.DarkRed;
        /// <summary>
        /// Critical颜色
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ConsoleColor Critical { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// 获得控制台颜色
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public ConsoleColor GetConsoleColor(LogLevel logLevel) => logLevel switch
        {
            LogLevel.Trace => Trace,
            LogLevel.Debug => Debug,
            LogLevel.Information => Information,
            LogLevel.Warning => Warning,
            LogLevel.Error => Error,
            LogLevel.Critical => Critical,
            _ => Default
        };
    }
}
