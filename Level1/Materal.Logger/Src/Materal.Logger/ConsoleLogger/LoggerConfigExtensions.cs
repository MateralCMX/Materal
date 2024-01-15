namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个控制台目标
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="format"></param>
        /// <param name="colors"></param>
        public static LoggerConfig AddConsoleTarget(this LoggerConfig loggerConfig, string name, string? format = null, Dictionary<LogLevel, ConsoleColor>? colors = null)
        {
            ConsoleLoggerTargetConfig target = new()
            {
                Name = name
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            if (colors is not null)
            {
                target.Colors = new ColorsConfig(colors);
            }
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
    }
}
