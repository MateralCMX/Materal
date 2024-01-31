using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient2
{
    public class Client02Event01Handler : IEventHandler<Event01>
    {
        public async Task HandleAsync(Event01 @event)
        {
            Console.WriteLine($"------------------{nameof(Client02Event01Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client02Event01Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
