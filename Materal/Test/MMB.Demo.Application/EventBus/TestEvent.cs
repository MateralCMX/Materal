using Materal.EventBus.Abstraction;

namespace MMB.Demo.Application.EventBus
{
    public class TestEvent : IEvent
    {
        public string Message { get; set; } = string.Empty;
    }
}
