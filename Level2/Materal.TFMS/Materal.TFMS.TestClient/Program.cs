using Materal.Logger;
using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Materal.TFMS.TestClient
{
    public class Program
    {
        public static async Task Main()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMateralLogger();
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole");
                option.AddAllTargetRule();
            });
            LoggerManager.CustomConfig.Add("ApplicationName", "TestClient");
            const string queueName = "Educational_FatQueue";
            const string exchangeName = "XMJEventBusExchange_Fat";
            MateralTFMSRabbitMQConfig.EventErrorConfig.Discard = false;
            services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => new ConnectionFactory
            {
                HostName = "175.27.194.19",
                Port = 5672,
                DispatchConsumersAsync = true,
                UserName = "GDB",
                Password = "GDB2022"
            });
            services.AddEventBusSubscriptionsManager().AddRabbitMQPersistentConnection();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                IRabbitMQPersistentConnection? rabbitMQPersistentConnection = serviceProvider.GetRequiredService<IRabbitMQPersistentConnection>();
                ILogger<EventBusRabbitMQ>? logger = serviceProvider.GetService<ILogger<EventBusRabbitMQ>>();
                IEventBusSubscriptionsManager? eventBusSubscriptionsManager = serviceProvider.GetRequiredService<IEventBusSubscriptionsManager>();
                return new EventBusRabbitMQ(rabbitMQPersistentConnection, serviceProvider, eventBusSubscriptionsManager, queueName, exchangeName, false, logger);
            });
            services.AddTransient<NewRegistrationFormEventHandler>();
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IEventBus eventBus = serviceProvider.GetRequiredService<IEventBus>();
            await eventBus.SubscribeAsync<NewRegistrationFormEvent, NewRegistrationFormEventHandler>();
            eventBus.StartListening();
            Console.ReadKey();
        }
    }
}