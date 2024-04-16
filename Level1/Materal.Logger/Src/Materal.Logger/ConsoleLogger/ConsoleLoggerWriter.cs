namespace Materal.Logger.ConsoleLogger
{
    /// <summary>
    /// 控制台日志写入器
    /// </summary>
    public class ConsoleLoggerWriter(IOptionsMonitor<LoggerOptions> options) : BaseLoggerWriter<ConsoleLoggerTargetOptions>
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
            log.Message = log.ApplyText(log.Message, options.CurrentValue);
            ConsoleColor color = targetOptions.Colors.GetConsoleColor(log.Level);
            ConsoleQueue.WriteLine(log.Message, color);
            await Task.CompletedTask;
        }
    }
}
