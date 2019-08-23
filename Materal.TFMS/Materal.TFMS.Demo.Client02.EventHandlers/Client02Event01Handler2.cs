using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;
using System;
using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Client02.EventHandlers
{
    public class Client02Event01Handler2 : IIntegrationEventHandler<Event01>
    {
        public async Task HandleAsync(Event01 @event)
        {
            await Task.Run(() => Console.WriteLine($"{GetType().Name}接收到事件{@event.GetType().Name}"));
        }
    }
}
