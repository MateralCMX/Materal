using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;
using Microsoft.Extensions.Logging;

namespace Materal.EventBus.TestClient
{
    public class Event01Handler(ILogger<Event01Handler>? logger = null) : BaseEventHandler<Event01>
    {
        public override async Task HandleAsync(Event01 @event)
        {
            logger?.LogInformation($"接收到消息:{@event.Message}");
            await Task.CompletedTask;
        }
    }
}
