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
            eventBus.Subscribe<Event01, Client03Event01Handler>();
            eventBus.Subscribe<Event01, Client03Event01Handler2>();
            eventBus.Subscribe<Event02, Client03Event02Handler>();
            eventBus.Subscribe<Event02, Client03Event02Handler2>();
        }

        public void SendEvent()
        {
            var @event = new Event03
            {
                Message = "这是来自Client03的事件"
            };
            _eventBus.Publish(@event);
        }
    }
}
