using Microsoft.Extensions.Logging;

namespace Materal.Logger.Models
{
    public class MateralLoggerColorsConfigModel
    {
        public static ConsoleColor Default => ConsoleColor.Gray;
        public ConsoleColor Trace { get; set; } = ConsoleColor.Gray;
        public ConsoleColor Debug { get; set; } = ConsoleColor.Gray;
        public ConsoleColor Information { get; set; } = ConsoleColor.Gray;
        public ConsoleColor Warning { get; set; } = ConsoleColor.Gray;
        public ConsoleColor Error { get; set; } = ConsoleColor.Gray;
        public ConsoleColor Critical { get; set; } = ConsoleColor.Gray;
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
