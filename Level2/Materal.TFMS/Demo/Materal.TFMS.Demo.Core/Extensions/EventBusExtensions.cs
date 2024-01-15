using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Materal.TFMS.Demo.Core.Extensions
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName, string exchangeName = "MateralTFMSEventBusExchange")
        {
            MateralTFMSRabbitMQConfig.EventErrorConfig.Discard = false;
            services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => ConnectionHelper.GetConnectionFactory());
            services.AddEventBusSubscriptionsManager().AddRabbitMQPersistentConnection();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                IRabbitMQPersistentConnection? rabbitMQPersistentConnection = serviceProvider.GetService<IRabbitMQPersistentConnection>() ?? throw new ApplicationException("获取服务失败");
                ILogger<EventBusRabbitMQ>? logger = serviceProvider.GetService<ILogger<EventBusRabbitMQ>>();
                IEventBusSubscriptionsManager? eventBusSubscriptionsManager = serviceProvider.GetService<IEventBusSubscriptionsManager>() ?? throw new ApplicationException("获取服务失败");
                return new EventBusRabbitMQ(rabbitMQPersistentConnection, serviceProvider, eventBusSubscriptionsManager, queueName, exchangeName, false, logger);
            });
            return services;
        }
    }
}
