﻿using Materal.TFMS.Demo.Client02.EventHandlers;
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
            SubscribeAsync(_eventBus).Wait();
            _eventBus.StartListening();
        }
        private static async Task SubscribeAsync(IEventBus eventBus)
        {
            await eventBus.SubscribeAsync<Event01, Client02Event01Handler>();
            await eventBus.SubscribeAsync<Event01, Client02Event01Handler2>();
            await eventBus.SubscribeAsync<Event03, Client02Event03Handler>();
            await eventBus.SubscribeAsync<Event03, Client02Event03Handler2>();
        }

        public async Task SendEventAsync()
        {
            var @event = new Event02
            {
                Message = $"这是来自Client02的{nameof(Event02)}事件"
            };
            await _eventBus.PublishAsync(@event);
        }
    }
}
