﻿using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient3
{
    public class Client03Event01Handler : BaseEventHandler<Event01>
    {
        public override async Task HandleAsync(Event01 @event)
        {
            Console.WriteLine($"------------------{nameof(Client03Event01Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client03Event01Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
