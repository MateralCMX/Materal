using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;
using System;
using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Client02.EventHandlers
{
    public class Client02Event03Handler2 : IIntegrationEventHandler<Event03>
    {
        public async Task HandleAsync(Event03 @event)
        {
            await Task.Run(() => Console.WriteLine($"{GetType().Name}接收到事件{@event.GetType().Name}"));
        }
    }
}
