namespace Materal.Logger.Message
{
    public class BaseEvent : IEvent
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string EventName { get; set; }
        public BaseEvent()
        {
            if (string.IsNullOrWhiteSpace(EventName))
            {
                EventName = GetType().Name;
            }
        }
        public BaseEvent(string eventName)
        {
            EventName = eventName;
        }
    }
}
