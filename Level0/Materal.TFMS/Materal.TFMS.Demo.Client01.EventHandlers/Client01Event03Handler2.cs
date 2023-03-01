using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;
using System;
using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Client01.EventHandlers
{
    public class Client01Event03Handler2 : IIntegrationEventHandler<Event03>
    {
        public Task HandleAsync(Event03 @event)
        {
            Console.WriteLine($"{GetType().Name}接收到事件{@event.GetType().Name}");
            return Task.FromResult(0);
        }
    }
}
