using Materal.TFMS.EventBus;
using MMB.Demo.Abstractions.Events;

namespace MMB.Demo.Service.EventHandlers
{
    public class MyEventHandler : IIntegrationEventHandler<MyEvent>
    {
        public async Task HandleAsync(MyEvent @event)
        {
            Console.WriteLine(@event.Message);
            await Task.CompletedTask;
        }
    }
}
