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
        public ConsoleColor Trace { get; set; } = ConsoleColor.DarkGray;
        /// <summary>
        /// Debug颜色
        /// </summary>
        public ConsoleColor Debug { get; set; } = ConsoleColor.DarkGreen;
        /// <summary>
        /// Information颜色
        /// </summary>
        public ConsoleColor Information { get; set; } = ConsoleColor.Gray;
        /// <summary>
        /// Warning颜色
        /// </summary>
        public ConsoleColor Warning { get; set; } = ConsoleColor.DarkYellow;
        /// <summary>
        /// Error颜色
        /// </summary>
        public ConsoleColor Error { get; set; } = ConsoleColor.DarkRed;
        /// <summary>
        /// Critical颜色
        /// </summary>
        public ConsoleColor Critical { get; set; } = ConsoleColor.Red;
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerColorsConfigModel() { }
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoggerColorsConfigModel(Dictionary<LogLevel, ConsoleColor> colors)
        {
            foreach (KeyValuePair<LogLevel, ConsoleColor> color in colors)
            {
                switch (color.Key)
                {
                    case LogLevel.Trace:
                        Trace = color.Value;
                        break;
                    case LogLevel.Debug:
                        Debug = color.Value;
                        break;
                    case LogLevel.Information:
                        Information = color.Value;
                        break;
                    case LogLevel.Warning:
                        Warning = color.Value;
                        break;
                    case LogLevel.Error:
                        Error = color.Value;
                        break;
                    case LogLevel.Critical:
                        Critical = color.Value;
                        break;
                }
            }
        }
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
