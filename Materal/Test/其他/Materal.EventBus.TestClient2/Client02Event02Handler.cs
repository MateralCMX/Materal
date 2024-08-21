using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;

namespace Materal.EventBus.TestClient2
{
    public class Client02Event02Handler : BaseEventHandler<Event02>, IEventHandler<Event02>
    {
        public override async Task HandleAsync(Event02 @event)
        {
            Console.WriteLine($"------------------{nameof(Client02Event02Handler)}---------------------");
            Console.WriteLine(@event.Message);
            Console.WriteLine($"------------------{nameof(Client02Event02Handler)}---------------------");
            await Task.CompletedTask;
        }
    }
}
