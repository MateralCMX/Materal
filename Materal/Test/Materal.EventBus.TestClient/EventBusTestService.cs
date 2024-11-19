using Materal.EventBus.Abstraction;
using Materal.EventBus.TestClient.Abstraction;
using Microsoft.Extensions.Hosting;

namespace Materal.EventBus.TestClient
{
    internal class EventBusTestService(IEventBus eventBus) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (Console.ReadLine() != "Exit")
            {
                if (cancellationToken.IsCancellationRequested) break;
                await eventBus.PublishAsync(new Event01 { Message = "Hello Event01" });
                await eventBus.PublishAsync(new Event02 { Message = "Hello Event02" });
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;
    }
}
