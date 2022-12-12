using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.TFMS.Demo.Core.Extensions
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName, string exchangeName = "MateralTFMSEventBusExchange")
        {
            services.AddEventBusSubscriptionsManager().AddRabbitMQPersistentConnection();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                var rabbitMQPersistentConnection = serviceProvider.GetService<IRabbitMQPersistentConnection>();
                ILogger<EventBusRabbitMQ> logger = serviceProvider.GetService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubscriptionsManager = serviceProvider.GetService<IEventBusSubscriptionsManager>();
                return new EventBusRabbitMQ(rabbitMQPersistentConnection, serviceProvider, eventBusSubscriptionsManager, queueName, exchangeName, false, logger);
            });
            return services;
        }
    }
}
