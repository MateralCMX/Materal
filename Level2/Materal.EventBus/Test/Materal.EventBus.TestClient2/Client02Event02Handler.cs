using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient2
{
    public class Client02Event02Handler : IEventHandler<Event02>
    {
        public async Task HandleAsync(Event02 @event)
        {
            Console.WriteLine($"------------------{nameof(Client02Event02Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client02Event02Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
