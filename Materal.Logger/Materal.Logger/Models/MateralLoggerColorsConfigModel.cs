using Microsoft.Extensions.Logging;

namespace Materal.Logger.Models
{
    public class MateralLoggerColorsConfigModel
    {
        public static ConsoleColor Default { get; set; }
        public ConsoleColor? Trace { get; set; }
        public ConsoleColor? Debug { get; set; }
        public ConsoleColor? Information { get; set; }
        public ConsoleColor? Warning { get; set; }
        public ConsoleColor? Error { get; set; }
        public ConsoleColor? Critical { get; set; }
        public ConsoleColor? None { get; set; }
        static MateralLoggerColorsConfigModel()
        {
            Default = Console.ForegroundColor;
        }
        /// <summary>
        /// 获得控制台颜色
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public ConsoleColor GetConsoleColor(LogLevel logLevel)
        {
            ConsoleColor? consoleColor = logLevel switch
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
            return consoleColor ?? Default;
        }
    }
}
