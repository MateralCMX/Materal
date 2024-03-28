
namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 总线日志写入器模型
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
            foreach (WeakReference<Action<LogModel>> handler in _globalHandlers)
            {
                try
                {
                    if (!handler.TryGetTarget(out Action<LogModel>? action) || action is null) continue;
                    action.Invoke(model);
                }
                catch(Exception ex)
                {
                    LoggerHost.LoggerLog?.LogWarning("BusLogger处理失败", ex);
                }
            }
            foreach (WeakReference<Func<LogModel, Task>>? handler in _globalAsyncHandlers)
            {
                if (handler is null) continue;
                try
                {
                    if (!handler.TryGetTarget(out Func<LogModel, Task>? action) || action is null) continue;
                    await action.Invoke(model);
                }
                catch (Exception ex)
                {
                    LoggerHost.LoggerLog?.LogWarning("BusLogger处理失败", ex);
                }
            }
            if (_handlers.ContainsKey(Target.Name))
            {
                foreach (WeakReference<Action<LogModel>> handler in _handlers[Target.Name])
                {
                    try
                    {
                        if (!handler.TryGetTarget(out Action<LogModel>? action) || action is null) continue;
                        action.Invoke(model);
                    }
                    catch (Exception ex)
                    {
                        LoggerHost.LoggerLog?.LogWarning("BusLogger处理失败", ex);
                    }
                }
            }
            if (_asyncHandlers.ContainsKey(Target.Name))
            {
                foreach (WeakReference<Func<LogModel, Task>>? handler in _asyncHandlers[Target.Name])
                {
                    if (handler is null) continue;
                    try
                    {
                        if (!handler.TryGetTarget(out Func<LogModel, Task>? action) || action is null) continue;
                        await action.Invoke(model);
                    }
                    catch (Exception ex)
                    {
                        LoggerHost.LoggerLog?.LogWarning("BusLogger处理失败", ex);
                    }
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
            IsClose = true;
            _globalHandlers.Clear();
            _globalAsyncHandlers.Clear();
            _handlers.Clear();
            _asyncHandlers.Clear();
            LoggerHost.LoggerLog?.LogDebug($"[{Target.Name}]关闭成功");
            await Task.CompletedTask;
        }
    }
}
