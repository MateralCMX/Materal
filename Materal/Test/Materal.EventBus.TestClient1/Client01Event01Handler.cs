﻿using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient1
{
    public class Client01Event01Handler : BaseEventHandler<Event01>
    {
        public string? QueueName => "MateralEventBusTestClient1_1Queue";

        public override async Task HandleAsync(Event01 @event)
        {
            Console.WriteLine($"------------------{nameof(Client01Event01Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client01Event01Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
