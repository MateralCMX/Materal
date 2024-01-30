using Materal.EventBus.Abstraction;
using Materal.EventBus.RabbitMQ;
using Materal.EventBus.TestClient.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Materal.EventBus.TestClient2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddRabbitMQEventBus(builder.Configuration.GetSection("EventBus"), config =>
            {
                config.QueueName = "MateralEventBusTestClient2Queue";
            }, typeof(Program).Assembly);
            IHost app = builder.Build();
            IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
            eventBus.Subscribe<Event01, Client02Event01Handler>();
            eventBus.Subscribe<Event02, Client02Event02Handler>();
            eventBus.Subscribe<Event03, Client02Event03Handler>();
            while (Console.ReadLine() != "Exit")
            {
                eventBus.Publish(new Event02 { Message = "Hello World" });
            }
            await app.RunAsync();
        }
    }
}
