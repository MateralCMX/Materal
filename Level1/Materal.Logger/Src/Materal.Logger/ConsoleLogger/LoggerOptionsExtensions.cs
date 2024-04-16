namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 自定义日志配置
    /// </summary>
    public static class LoggerOptionsExtensions
    {
        /// <summary>
        /// 添加一个控制台目标
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        public static LoggerOptions AddConsoleTarget(this LoggerOptions options, string name, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null)
        {
            ConsoleLoggerTargetOptions target = new()
            {
                Name = name
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            if (colors is not null)
            {
                target.Colors = new LoggerColorsOptions(colors);
            }
            options.AddTarget(target);
            return options;
        }
    }
}
