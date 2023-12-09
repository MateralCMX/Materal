using Materal.TFMS.EventBus;

namespace Materal.TFMS.Demo.Events
{
    public class MyEvent : IntegrationEvent
    {
        public string Message { get; set; } = string.Empty;
    }
}
