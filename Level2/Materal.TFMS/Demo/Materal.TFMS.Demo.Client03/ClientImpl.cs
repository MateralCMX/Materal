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
            SubscribeAsync(_eventBus).Wait();
            _eventBus.StartListening();
        }
        private static async Task SubscribeAsync(IEventBus eventBus)
        {
            await eventBus.SubscribeAsync<Event01, Client03Event01Handler>();
            await eventBus.SubscribeAsync<Event01, Client03Event01Handler2>();
            await eventBus.SubscribeAsync<Event02, Client03Event02Handler>();
            await eventBus.SubscribeAsync<Event02, Client03Event02Handler2>();
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
