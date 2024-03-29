﻿using Materal.TFMS.Demo.Client01.EventHandlers;
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
            SubscribeAsync(_eventBus).Wait();
            _eventBus.StartListening();
        }
        private static async Task SubscribeAsync(IEventBus eventBus)
        {
            await eventBus.SubscribeAsync(typeof(Event02), typeof(Client01Event02Handler));
            await eventBus.SubscribeAsync<Event02, Client01Event02Handler2>();
            await eventBus.SubscribeAsync<Event03, Client01Event03Handler>();
            await eventBus.SubscribeAsync<Event03, Client01Event03Handler2>();
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
