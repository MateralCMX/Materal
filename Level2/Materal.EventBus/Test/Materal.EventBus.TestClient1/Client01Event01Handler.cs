using Materal.EventBus.Abstraction;
using Materal.EventBus.RabbitMQ;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient1
{
    [QueueName("MateralEventBusTestClient1_1Queue")]
    public class Client01Event01Handler : IEventHandler<Event01>
    {
        public async Task HandlerAsync(Event01 @event)
        {
            Console.WriteLine($"------------------{nameof(Client01Event01Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client01Event01Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
