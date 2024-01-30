using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient1
{
    public class Client01Event03Handler : IEventHandler<Event03>
    {
        public async Task HandlerAsync(Event03 @event)
        {
            Console.WriteLine($"------------------{nameof(Client01Event03Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client01Event03Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
