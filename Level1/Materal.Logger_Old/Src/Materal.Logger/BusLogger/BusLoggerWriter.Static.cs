namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 总线日志写入器
    /// </summary>
    public partial class BusLoggerWriter
    {
        private readonly static List<WeakReference<ILogMonitor>> _globalHandlers = [];
        private readonly static Dictionary<string, List<WeakReference<ILogMonitor>>> _handlers = [];
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="busName"></param>
        public static void Subscribe(ILogMonitor monitor, string? busName = null)
        {
            if (busName is null || string.IsNullOrWhiteSpace(busName))
            {
                Subscribe(monitor, _globalHandlers);
            }
            else
            {
                if (!_handlers.TryGetValue(busName, out List<WeakReference<ILogMonitor>>? handlers))
                {
                    _handlers.Add(busName, [new(monitor)]);
                }
                else
                {
                    Subscribe(monitor, handlers);
                }
            }
        }
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="handlers"></param>
        private static void Subscribe(ILogMonitor monitor, List<WeakReference<ILogMonitor>> handlers)
        {
            if (!CanSubscribe(monitor, handlers)) return;
            handlers.Add(new(monitor));
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="busName"></param>
        public static void Unsubscribe(ILogMonitor monitor, string? busName = null)
        {
            if (busName is null || string.IsNullOrWhiteSpace(busName))
            {
                Unsubscribe(monitor, _globalHandlers);
            }
            else
            {
                if (!_handlers.TryGetValue(busName, out List<WeakReference<ILogMonitor>>? handlers)) return;
                Unsubscribe(monitor, handlers);
            }
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="handlers"></param>
        private static void Unsubscribe(ILogMonitor monitor, List<WeakReference<ILogMonitor>> handlers)
        {
            for (int i = 0; i < handlers.Count; i++)
            {
                WeakReference<ILogMonitor> item = handlers[i];
                if (!item.TryGetTarget(out ILogMonitor? target)) continue;
                if (!target.Equals(monitor)) continue;
                handlers.Remove(item);
                return;
            }
        }
        /// <summary>
        /// 取消所有订阅
        /// </summary>
        /// <param name="monitor"></param>
        public static void UnsubscribeAll(ILogMonitor monitor)
        {
            Unsubscribe(monitor, _globalHandlers);
            foreach (KeyValuePair<string, List<WeakReference<ILogMonitor>>> handlers in _handlers)
            {
                Unsubscribe(monitor, handlers.Value);
            }
        }
        /// <summary>
        /// 取消所有订阅
        /// </summary>
        public static void UnsubscribeAll()
        {
            _globalHandlers.Clear();
            _handlers.Clear();
        }
        /// <summary>
        /// 是否可以订阅
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="busName"></param>
        /// <returns></returns>
        public static bool CanSubscribe(ILogMonitor monitor, string? busName = null)
        {
            if (busName is null || string.IsNullOrWhiteSpace(busName))
            {
                return CanSubscribe(monitor, _globalHandlers);
            }
            else
            {
                if (!_handlers.TryGetValue(busName, out List<WeakReference<ILogMonitor>>? handlers))
                {
                    return true;
                }
                else
                {
                    return CanSubscribe(monitor, handlers);
                }
            }
        }
        /// <summary>
        /// 是否可以订阅
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="handlers"></param>
        private static bool CanSubscribe(ILogMonitor monitor, List<WeakReference<ILogMonitor>> handlers)
        {
            foreach (WeakReference<ILogMonitor> item in handlers)
            {
                if (!item.TryGetTarget(out ILogMonitor? target)) continue;
                if (target.Equals(monitor)) return false;
            }
            return true;
        }
    }
}
