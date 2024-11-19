using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;
using Microsoft.Extensions.Hosting;

namespace Materal.EventBus.TestClient1
{
    internal class EventBusTestService(IEventBus eventBus) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Press Enter to publish an event, type 'Exit' to exit.");
            while (Console.ReadLine() != "Exit")
            {
                if (cancellationToken.IsCancellationRequested) break;
                await eventBus.PublishAsync(new Event01 { Message = "Hello Event01" });
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
