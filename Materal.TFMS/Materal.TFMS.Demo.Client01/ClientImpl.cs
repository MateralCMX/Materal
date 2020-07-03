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
            eventBus.Subscribe<Event01, Client01Event01Handler>();
            //eventBus.Subscribe<Event02, Client01Event02Handler>();
            //eventBus.Subscribe<Event02, Client01Event02Handler2>();
            //eventBus.Subscribe<Event03, Client01Event03Handler>();
            //eventBus.Subscribe<Event03, Client01Event03Handler2>();
            //eventBus.Subscribe<Event04, Client01Event04Handler>();
            //eventBus.Subscribe<Event05, Client01Event05Handler>();
        }

        public void SendEvent()
        {
            var event1 = new Event01
            {
                Message = $"这是来自Client01的{nameof(Event01)}事件"
            };
            var event2 = new Event02
            {
                Message = $"这是来自Client01的{nameof(Event02)}事件"
            };
            var event3 = new Event03
            {
                Message = $"这是来自Client01的{nameof(Event03)}事件"
            };
            var event4 = new Event04
            {
                Message = $"这是来自Client01的{nameof(Event04)}事件"
            };
            var event5 = new Event05
            {
                Message = $"这是来自Client01的{nameof(Event05)}事件"
            };
            //for (var i = 0; i < 10; i++)
            //{
            //    _eventBus.Publish(event1);
            //    _eventBus.Publish(event2);
            //    _eventBus.Publish(event3);
            //    _eventBus.Publish(event4);
            //    _eventBus.Publish(event5);
            //}
            _eventBus.Publish(event1);
        }
    }
}
