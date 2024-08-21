namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    public class SubscriptionInfo(Type eventType)
    {
        /// <summary>
        /// 处理器类型
        /// </summary>
        public List<Type> HandlerTypes { get; } = [];
        /// <summary>
        /// 事件类型
        /// </summary>
        public Type EventType { get; } = eventType;
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; } = GetEventName(eventType);
        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsEmpty => HandlerTypes.Count == 0;
        /// <summary>
        /// 获得事件名称
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public static string GetEventName(Type eventType) => eventType.Name;
        /// <summary>
        /// 添加处理器
        /// </summary>
        /// <param name="handlerType"></param>
        public void AddHandler(Type handlerType)
        {
            if (HandlerTypes.Contains(handlerType)) return;
            HandlerTypes.Add(handlerType);
        }
    }
}
