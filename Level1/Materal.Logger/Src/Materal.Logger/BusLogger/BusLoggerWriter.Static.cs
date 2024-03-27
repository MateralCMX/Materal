namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 总线日志写入器模型
    /// </summary>
    public partial class BusLoggerWriter
    {
        private readonly static List<Action<BusLoggerWriterModel[]>> _globalHandlers = [];
        private readonly static List<Func<BusLoggerWriterModel[], Task>> _globalAsyncHandlers = [];
        private readonly static Dictionary<string, List<Action<BusLoggerWriterModel[]>>> _handlers = [];
        private readonly static Dictionary<string, List<Func<BusLoggerWriterModel[], Task>>> _asyncHandlers = [];
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static void SubscribeLogger(Action<BusLoggerWriterModel[]> handler, string? busWriterName = null)
            => SubscribeLogger(handler, _globalHandlers, _handlers, busWriterName);
        /// <summary>
        /// 订阅日志
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static void SubscribeLogger(Func<BusLoggerWriterModel[], Task> handler, string? busWriterName = null)
            => SubscribeLogger(handler, _globalAsyncHandlers, _asyncHandlers, busWriterName);
        /// <summary>
        /// 取消订阅日志
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static void UnsubscribeLogger(Action<BusLoggerWriterModel[]> handler, string? busWriterName = null)
            => UnsubscribeLogger(handler, _globalHandlers, _handlers, busWriterName);
        /// <summary>
        /// 取消订阅日志
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="busWriterName"></param>
        public static void UnsubscribeLogger(Func<BusLoggerWriterModel[], Task> handler, string? busWriterName = null)
            => UnsubscribeLogger(handler, _globalAsyncHandlers, _asyncHandlers, busWriterName);
        /// <summary>
        /// 取消订阅所有日志
        /// </summary>
        public static void UnsubscribeAllLogger()
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
        private static void SubscribeLogger<T>(T handler, List<T> globalHandlers, Dictionary<string, List<T>> handlers, string? busWriterName)
        {
            if (busWriterName is null || string.IsNullOrWhiteSpace(busWriterName))
            {
                if (globalHandlers.Contains(handler)) return;
                globalHandlers.Add(handler);
            }
            else
            {
                if (!handlers.ContainsKey(busWriterName))
                {
                    handlers.Add(busWriterName, [handler]);
                }
                else
                {
                    if (handlers[busWriterName].Contains(handler)) return;
                    handlers[busWriterName].Add(handler);
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
        private static void UnsubscribeLogger<T>(T handler, List<T> globalHandlers, Dictionary<string, List<T>> handlers, string? busWriterName)
        {
            if (busWriterName is null || string.IsNullOrWhiteSpace(busWriterName))
            {
                if (!globalHandlers.Contains(handler)) return;
                globalHandlers.Remove(handler);
            }
            else
            {
                if (!handlers.ContainsKey(busWriterName) || !handlers[busWriterName].Contains(handler)) return;
                handlers[busWriterName].Remove(handler);
            }
        }
    }
}
