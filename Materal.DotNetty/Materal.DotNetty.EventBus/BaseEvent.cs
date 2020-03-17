using System;

namespace Materal.DotNetty.EventBus
{
    [Serializable]
    public class BaseEvent : IEvent
    {
        public string Event { get; set; }

        public string EventHandler => $"{Event}Handler";
    }
}
