using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient3
{
    public class Client03Event01Handler : IEventHandler<Event01>
    {
        public async Task HandlerAsync(Event01 @event)
        {
            Console.WriteLine($"------------------{nameof(Client03Event01Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client03Event01Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
