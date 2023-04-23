using Materal.TFMS.Demo.Client03.EventHandlers;
using Materal.TFMS.Demo.Core;
using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;

namespace Materal.TFMS.Demo.Client03
{
    public class ClientImpl : IClient
    {
        private readonly IEventBus _eventBus;

        public ClientImpl(IEventBus eventBus)
        {
            _eventBus = eventBus;
            eventBus.SubscribeAsync<Event01, Client03Event01Handler>();
            eventBus.SubscribeAsync<Event01, Client03Event01Handler2>();
            eventBus.SubscribeAsync<Event02, Client03Event02Handler>();
            eventBus.SubscribeAsync<Event02, Client03Event02Handler2>();
            _eventBus.StartListening();
        }
        public async Task SendEventAsync()
        {
            var @event = new Event03
            {
                Message = $"这是来自Client02的{nameof(Event03)}事件"
            };
            await _eventBus.PublishAsync(@event);
        }
    }
}
