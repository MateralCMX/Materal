using Materal.WebSocket.Events;

namespace TestClient.Events
{
    public class Event: IEvent
    {
        public Event(string handlerName)
        {
            HandlerName = handlerName;
        }
        public string HandlerName { get; }
        public string StringData { get; set; }
        public byte[] ByteArrayData { get; set; }
        public string Message { get; set; }
    }
}
