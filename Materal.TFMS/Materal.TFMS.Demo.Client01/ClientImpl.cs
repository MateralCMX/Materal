using System.Threading.Tasks;
using Materal.TFMS.Demo.Client01.EventHandlers;
using Materal.TFMS.Demo.Core;
using Materal.TFMS.Demo.Events;
using Materal.TFMS.EventBus;

namespace Materal.TFMS.Demo.Client01
{
    public class ClientImpl : IClient
    {
        private readonly IEventBus _eventBus;

        public ClientImpl(IEventBus eventBus)
        {
            _eventBus = eventBus;
            //eventBus.SubscribeAsync<Event01, Client01Event01Handler>();
            eventBus.SubscribeAsync<Event02, Client01Event02Handler>();
            eventBus.SubscribeAsync<Event02, Client01Event02Handler2>();
            eventBus.SubscribeAsync<Event03, Client01Event03Handler>();
            eventBus.SubscribeAsync<Event03, Client01Event03Handler2>();
            _eventBus.StartListening();
        }

        public async Task SendEventAsync()
        {
            var event1 = new Event01
            {
                Message = $"这是来自Client01的{nameof(Event01)}事件"
            };
            await _eventBus.PublishAsync(event1);
        }
    }
}
