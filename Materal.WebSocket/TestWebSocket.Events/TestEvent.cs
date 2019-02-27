namespace TestWebSocket.Events
{
    public class TestEvent : Event
    {
        public TestEvent() : base("TestEventHandler")
        {
        }
        public string StringData { get; set; }
    }
}
