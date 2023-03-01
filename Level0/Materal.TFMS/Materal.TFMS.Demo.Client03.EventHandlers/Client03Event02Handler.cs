using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;
using System;
using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Client03.EventHandlers
{
    public class Client03Event02Handler : IIntegrationEventHandler<Event02>
    {
        public async Task HandleAsync(Event02 @event)
        {
            await Task.Run(() => Console.WriteLine($"{GetType().Name}接收到事件{@event.GetType().Name}"));
        }
    }
}
