using Materal.APP.Core.ConfigModels;
using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Reflection;

namespace Materal.APP.TFMS.Core
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName, string exchangeName = "MateralTFMSDemoExchangeName")
        {
            services.AddEventBusSubscriptionsManager().AddRabbitMQPersistentConnection();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                IRabbitMQPersistentConnection rabbitMQPersistentConnection = serviceProvider.GetService<IRabbitMQPersistentConnection>();
                ILogger<EventBusRabbitMQ> logger = serviceProvider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                IEventBusSubscriptionsManager eventBusSubscriptionsManager = serviceProvider.GetService<IEventBusSubscriptionsManager>();
                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, serviceProvider, eventBusSubscriptionsManager, queueName, exchangeName, false);
            });
            return services;
        }
        public static IServiceCollection AddEventConnectionFactory(this IServiceCollection services, TFMSConfigModel tfmsConfig)
        {
            services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => new ConnectionFactory
            {
                HostName = tfmsConfig.Host,
                Port = tfmsConfig.Port,
                DispatchConsumersAsync = true,
                UserName = tfmsConfig.UserName,
                Password = tfmsConfig.Password
            });
            return services;
        }
        public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type handlerType in assembly.ExportedTypes)
                {
                    services.AddTransient(handlerType);
                }
            }
            return services;
        }
    }
}
