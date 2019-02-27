using Materal.WebSocket.Events;

namespace TestWebSocket.Events
{
    public class Event: IEvent
    {
        public Event()
        {
        }
        public Event(string handlerName)
        {
            HandlerName = handlerName;
        }
        public string HandlerName { get; }
    }
}
