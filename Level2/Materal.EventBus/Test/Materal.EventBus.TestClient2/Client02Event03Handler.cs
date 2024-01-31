using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient2
{
    public class Client02Event03Handler : IEventHandler<Event03>
    {
        public async Task HandleAsync(Event03 @event)
        {
            Console.WriteLine($"------------------{nameof(Client02Event03Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client02Event03Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
