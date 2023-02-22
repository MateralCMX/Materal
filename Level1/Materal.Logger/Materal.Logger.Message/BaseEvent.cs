namespace Materal.Logger.Message
{
    /// <summary>
    /// 基事件
    /// </summary>
    public class BaseEvent : IEvent
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string EventName { get; set; } = string.Empty;
        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseEvent()
        {
            if (string.IsNullOrWhiteSpace(EventName))
            {
                EventName = GetType().Name;
            }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="eventName"></param>
        public BaseEvent(string eventName)
        {
            EventName = eventName;
        }
    }
}
