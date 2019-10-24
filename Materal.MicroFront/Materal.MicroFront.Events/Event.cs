using Materal.WebSocket.Events;

namespace Materal.MicroFront.Events
{
    public class Event : IEvent
    {
        public Event()
        {
            HandlerName = GetType().Name + "Handler";
        }
        public Event(string handlerName)
        {
            HandlerName = handlerName;
        }
        public string HandlerName { get; set; }
    }
}
