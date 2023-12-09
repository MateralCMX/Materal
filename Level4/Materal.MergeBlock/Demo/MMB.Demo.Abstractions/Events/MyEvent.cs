using Materal.TFMS.EventBus;

namespace MMB.Demo.Abstractions.Events
{
    public class MyEvent : IntegrationEvent
    {
        public string Message { get; set; } = string.Empty;
    }
}
