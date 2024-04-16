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
            Dictionary<string, object?> data = [];
            data.Add("Message", log.Message);
            string logInfo = log.ApplyText(targetOptions.Format, options.CurrentValue, data);
            ConsoleColor color = targetOptions.Colors.GetConsoleColor(log.Level);
            ConsoleQueue.WriteLine(logInfo, color);
            await Task.CompletedTask;
        }
    }
}
