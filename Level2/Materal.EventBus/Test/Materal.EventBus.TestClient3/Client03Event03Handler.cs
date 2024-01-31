using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient3
{
    public class Client03Event03Handler : IEventHandler<Event03>
    {
        public async Task HandleAsync(Event03 @event)
        {
            Console.WriteLine($"------------------{nameof(Client03Event03Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client03Event03Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
