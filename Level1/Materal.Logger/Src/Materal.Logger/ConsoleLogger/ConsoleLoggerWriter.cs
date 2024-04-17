namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 控制台日志写入器
    /// </summary>
    public class ConsoleLoggerWriter(IOptionsMonitor<LoggerOptions> options) : BaseLoggerWriter<ConsoleLoggerTargetOptions>(options)
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        /// <returns></returns>
        public override async Task LogAsync(Log log, LoggerRuleOptions ruleOptions, ConsoleLoggerTargetOptions targetOptions)
        {
            string result = log.ApplyText(targetOptions.Format, Options.CurrentValue);
            if (result.EndsWith("\r\n"))
            {
                result = result[1..^2];
            }
            ConsoleColor color = targetOptions.Colors.GetConsoleColor(log.Level);
            ConsoleQueue.WriteLine(result, color);
            await Task.CompletedTask;
        }
    }
}
