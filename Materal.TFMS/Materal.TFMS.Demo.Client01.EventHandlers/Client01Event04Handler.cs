using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;
using System;
using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Client01.EventHandlers
{
    public class Client01Event04Handler : IIntegrationEventHandler<Event04>
    {
        public Task HandleAsync(Event04 @event)
        {
            Console.WriteLine($"{GetType().Name}接收到事件{@event.GetType().Name}");
            return Task.FromResult(0);
        }
    }
}
