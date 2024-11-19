using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;
using Microsoft.Extensions.Logging;

namespace Materal.EventBus.TestClient
{
    public class Event02Handler(ILogger<Event02Handler>? logger = null) : BaseEventHandler<Event02>
    {
        public override async Task HandleAsync(Event02 @event)
        {
            logger?.LogInformation($"接收到消息:{@event.Message}");
            await Task.CompletedTask;
        }
    }
}
