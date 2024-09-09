using Materal.EventBus.Abstraction;

namespace MMB.Demo.Application.EventBus
{
    public class TestEventHandler(ILogger<TestEventHandler>? logger = null) : BaseEventHandler<TestEvent>, IEventHandler<TestEvent>
    {
        public override async Task HandleAsync(TestEvent @event)
        {
            logger?.LogInformation(@event.Message);
            await Task.CompletedTask;
        }
    }
}
