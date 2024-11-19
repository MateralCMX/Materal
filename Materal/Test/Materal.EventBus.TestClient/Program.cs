using Materal.EventBus.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.EventBus.TestClient
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddMemoryEventBus(typeof(Program).Assembly);
            builder.Services.AddHostedService<EventBusTestService>();
            IHost app = builder.Build();
            await app.RunAsync();
        }
    }
}
