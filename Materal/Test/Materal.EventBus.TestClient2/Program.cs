using Materal.EventBus.Abstraction;
using Materal.EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.EventBus.TestClient2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Client2");
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddRabbitMQEventBus(builder.Configuration.GetSection("EventBus"), config =>
            {
                config.QueueName = "MateralEventBusTestClient2Queue";
            }, typeof(Program).Assembly);
            builder.Services.AddHostedService<EventBusTestService>();
            IHost app = builder.Build();
            IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
            await app.RunAsync();
        }
    }
}
