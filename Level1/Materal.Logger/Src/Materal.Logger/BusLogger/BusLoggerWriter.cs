namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 总线日志写入器
    /// </summary>
    public partial class BusLoggerWriter(IOptionsMonitor<LoggerOptions> options) : BaseLoggerWriter<BusLoggerTargetOptions>(options)
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ruleOptions"></param>
        /// <param name="targetOptions"></param>
        /// <returns></returns>
        public override async Task LogAsync(Log log, LoggerRuleOptions ruleOptions, BusLoggerTargetOptions targetOptions)
        {
            log.Message = log.ApplyText(log.Message, Options.CurrentValue);
            WriteLoggerAsync(log, _globalHandlers);
            if (!_handlers.TryGetValue(targetOptions.Name, out List<WeakReference<ILogMonitor>>? handlers) || handlers is null || handlers.Count <= 0) return;
            WriteLoggerAsync(log, handlers);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="handlers"></param>
        /// <returns></returns>
        public static async void WriteLoggerAsync(Log log, List<WeakReference<ILogMonitor>> handlers)
        {
            if (handlers.Count <= 0) return;
            for (int i = 0; i < handlers.Count; i++)
            {
                WeakReference<ILogMonitor> handler = handlers[i];
                if (!handler.TryGetTarget(out ILogMonitor? monitor) || monitor is null) return;
                await monitor.OnLogAsync(log);
            }
        }
    }
}
