using System.Threading.Tasks;
using Materal.TFMS.Demo.Client02.EventHandlers;
using Materal.TFMS.Demo.Core;
using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;

namespace Materal.TFMS.Demo.Client02
{
    public class ClientImpl : IClient
    {
        private readonly IEventBus _eventBus;

        public ClientImpl(IEventBus eventBus)
        {
            _eventBus = eventBus;
            eventBus.SubscribeAsync<Event01, Client02Event01Handler>();
            eventBus.SubscribeAsync<Event01, Client02Event01Handler2>();
            eventBus.SubscribeAsync<Event03, Client02Event03Handler>();
            eventBus.SubscribeAsync<Event03, Client02Event03Handler2>();
            _eventBus.StartListening();
        }

        public async Task SendEventAsync()
        {
            var @event = new Event02
            {
                Message = "这是来自Client02的事件"
            };
            await _eventBus.PublishAsync(@event);
        }
    }
}
