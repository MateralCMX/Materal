using Materal.EventBus.Abstraction;
using Materal.EventBus.RabbitMQ;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient1
{
    public class Client01Event03Handler : BaseEventHandler<Event03>, IRabbitMQEventHandler
    {
        public string QueueName => "MateralEventBusTestClient1Queue3";
        public override async Task HandleAsync(Event03 @event)
        {
            Console.WriteLine($"------------------{nameof(Client01Event03Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client01Event03Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
