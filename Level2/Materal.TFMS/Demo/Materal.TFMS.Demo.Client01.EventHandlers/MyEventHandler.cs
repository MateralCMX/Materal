using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;
using System;
using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Client01.EventHandlers
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
