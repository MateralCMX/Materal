namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 总线日志写入器模型
    /// </summary>
    public partial class BusLoggerWriter
    {
        private readonly static List<WeakReference<Action<LogModel>>> _globalHandlers = [];
        private readonly static List<WeakReference<Func<LogModel, Task>>> _globalAsyncHandlers = [];
        private readonly static Dictionary<string, List<WeakReference<Action<LogModel>>>> _handlers = [];
        private readonly static Dictionary<string, List<WeakReference<Func<LogModel, Task>>>> _asyncHandlers = [];
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static void Subscribe(Action<LogModel> handler, string? busWriterName = null)
            => SubscribeLogger(handler, _globalHandlers, _handlers, busWriterName);
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static void Subscribe(Func<LogModel, Task> handler, string? busWriterName = null)
            => SubscribeLogger(handler, _globalAsyncHandlers, _asyncHandlers, busWriterName);
        /// <summary>
        /// 取消订阅日志
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static void Unsubscribe(Action<LogModel> handler, string? busWriterName = null)
            => UnsubscribeLogger(handler, _globalHandlers, _handlers, busWriterName);
        /// <summary>
        /// 取消订阅日志
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static void Unsubscribe(Func<LogModel, Task> handler, string? busWriterName = null)
            => UnsubscribeLogger(handler, _globalAsyncHandlers, _asyncHandlers, busWriterName);
        /// <summary>
        /// 取消订阅所有日志
        /// </summary>
        public static void UnsubscribeAll()
        {
            _globalHandlers.Clear();
            _globalAsyncHandlers.Clear();
            _handlers.Clear();
            _asyncHandlers.Clear();
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="globalHandlers"></param>
        /// <param name="handlers"></param>
        /// <param name="busWriterName"></param>
        private static void SubscribeLogger<T>(T handler, List<WeakReference<T>> globalHandlers, Dictionary<string, List<WeakReference<T>>> handlers, string? busWriterName)
            where T : class
        {
            if (busWriterName is null || string.IsNullOrWhiteSpace(busWriterName))
            {
                foreach (WeakReference<T> item in globalHandlers)
                {
                    if (!item.TryGetTarget(out T? target)) continue;
                    if (target == handler) return;
                }
                globalHandlers.Add(new(handler));
            }
            else
            {
                if (!handlers.TryGetValue(busWriterName, out List<WeakReference<T>>? value))
                {
                    handlers.Add(busWriterName, [new(handler)]);
                }
                else
                {
                    foreach (WeakReference<T> item in value)
                    {
                        if (!item.TryGetTarget(out T? target)) continue;
                        if (target == handler) return;
                    }
                    value.Add(new(handler));
                }
            }
        }
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler"></param>
        /// <param name="globalHandlers"></param>
        /// <param name="handlers"></param>
        /// <param name="busWriterName"></param>
        private static void UnsubscribeLogger<T>(T handler, List<WeakReference<T>> globalHandlers, Dictionary<string, List<WeakReference<T>>> handlers, string? busWriterName)
            where T : class
        {
            if (busWriterName is null || string.IsNullOrWhiteSpace(busWriterName))
            {
                for (int i = 0; i < globalHandlers.Count; i++)
                {
                    WeakReference<T> item = globalHandlers[i];
                    if (!item.TryGetTarget(out T? target) || target != handler) continue;
                    globalHandlers.Remove(item);
                    return;
                }
            }
            else
            {
                if (!handlers.ContainsKey(busWriterName)) return;
                for (int i = 0; i < handlers[busWriterName].Count; i++)
                {
                    WeakReference<T> item = handlers[busWriterName][i];
                    if (!item.TryGetTarget(out T? target) || target != handler) continue;
                    globalHandlers.Remove(item);
                    return;
                }
            }
        }
    }
}
