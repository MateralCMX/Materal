using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient1
{
    public class Client01Event02Handler : IEventHandler<Event02>
    {
        public async Task HandlerAsync(Event02 @event)
        {
            Console.WriteLine($"------------------{nameof(Client01Event02Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client01Event02Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
