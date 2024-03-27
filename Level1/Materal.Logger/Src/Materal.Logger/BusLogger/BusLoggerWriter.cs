
namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 总线日志写入器模型
    /// </summary>
    public partial class BusLoggerWriter(BusLoggerTargetConfig targetConfig) : BatchLoggerWriter<BusLoggerWriterModel, BusLoggerTargetConfig>(targetConfig), ILoggerWriter
    {
        /// <summary>
        /// 写入批量日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public override async Task WriteBatchLoggerAsync(BusLoggerWriterModel[] models)
        {
            foreach (Action<BusLoggerWriterModel[]> handler in _globalHandlers)
            {
                handler?.Invoke(models);
            }
            foreach (Func<BusLoggerWriterModel[], Task> handler in _globalAsyncHandlers)
            {
                if (handler is null) continue;
                await handler.Invoke(models);
            }
            if (_handlers.ContainsKey(Target.Name))
            {
                foreach (Action<BusLoggerWriterModel[]> handler in _handlers[Target.Name])
                {
                    handler?.Invoke(models);
                }
            }
            if (_asyncHandlers.ContainsKey(Target.Name))
            {
                foreach (Func<BusLoggerWriterModel[], Task> handler in _asyncHandlers[Target.Name])
                {
                    if (handler is null) continue;
                    await handler.Invoke(models);
                }
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public override async Task ShutdownAsync()
        {
            LoggerHost.LoggerLog?.LogDebug($"正在关闭[{Target.Name}]");
            await ShutdownBatchLoggerWriterAsync();
            _globalHandlers.Clear();
            _globalAsyncHandlers.Clear();
            _handlers.Clear();
            _asyncHandlers.Clear();
            LoggerHost.LoggerLog?.LogDebug($"[{Target.Name}]关闭成功");
        }
    }
}
