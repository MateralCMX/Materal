using Materal.EventBus.Abstraction;
using Materal.EventBus.RabbitMQ;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient1
{
    [QueueName("MateralEventBusTestClient1Queue2")]
    public class Client01Event02Handler : BaseEventHandler<Event02>
    {
        public override async Task HandleAsync(Event02 @event)
        {
            Console.WriteLine($"------------------{nameof(Client01Event02Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client01Event02Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
