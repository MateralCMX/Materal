namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 总线日志写入器
    /// </summary>
    public partial class BusLoggerWriter(BusLoggerTargetConfig targetConfig) : BaseLoggerWriter<BusLoggerWriterModel, BusLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task WriteLoggerAsync(BusLoggerWriterModel model)
        {
            WriteLoggerAsync(model, _globalHandlers);
            if (!_handlers.TryGetValue(Target.Name, out List<WeakReference<ILogMonitor>>? handlers) || handlers is null || handlers.Count <= 0) return;
            WriteLoggerAsync(model, handlers);
            await Task.CompletedTask;
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="handlers"></param>
        /// <returns></returns>
        public static async void WriteLoggerAsync(BusLoggerWriterModel model, List<WeakReference<ILogMonitor>> handlers)
        {
            if (handlers.Count <= 0) return;
            for (int i = 0; i < handlers.Count; i++)
            {
                WeakReference<ILogMonitor> handler = handlers[i];
                if (!handler.TryGetTarget(out ILogMonitor? monitor) || monitor is null) return;
                await monitor.HandlerNewLogInfoAsync(model);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public override async Task ShutdownAsync()
        {
            LoggerHost.LoggerLog?.LogDebug($"正在关闭[{Target.Name}]");
            IsClose = true;
            UnsubscribeAll();
            LoggerHost.LoggerLog?.LogDebug($"[{Target.Name}]关闭成功");
            await Task.CompletedTask;
        }
    }
}
