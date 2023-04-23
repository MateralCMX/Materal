using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;
using System;
using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Client03.EventHandlers
{
    public class Client03Event01Handler : IIntegrationEventHandler<Event01>
    {
        public async Task HandleAsync(Event01 @event)
        {
            await Task.Run(() => Console.WriteLine($"{GetType().Name}接收到事件{@event.GetType().Name}"));
        }
    }
}
