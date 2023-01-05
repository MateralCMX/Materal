using Materal.CacheHelper;
using Materal.Common;
using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RC.Core.Common;
using System.Reflection;

namespace RC.Core
{
    public static class RCDIExtension
    {
        public static IServiceCollection AddRCServices(this IServiceCollection services, params Assembly[] autoMapperAssemblys)
        {
            MateralConfig.PageStartNumber = 1;
            services.AddMemoryCache();
            services.AddSingleton<ICacheManager, MemoryCacheManager>();
            services.AddAutoMapper(config =>
            {
                config.AllowNullCollections = true;
            }, autoMapperAssemblys);
            return services;
        }
        public static IServiceCollection AddIntegrationEventBus(this IServiceCollection services, string queueName)
        {
            services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => new ConnectionFactory
            {
                HostName = RCConfig.EventBusConfig.HostName,
                Port = RCConfig.EventBusConfig.Port,
                DispatchConsumersAsync = true,
                UserName = RCConfig.EventBusConfig.UserName,
                Password = RCConfig.EventBusConfig.Password
            });
            MateralTFMSRabbitMQConfig.EventErrorConfig.Discard = true;//消息处理失败后是否丢弃消息
            MateralTFMSRabbitMQConfig.EventErrorConfig.IsRetry = false;//消息处理失败后是否重试
            MateralTFMSRabbitMQConfig.EventErrorConfig.RetryNumber = 5;//消息处理失败后重试次数
            services.AddEventBusSubscriptionsManager().AddRabbitMQPersistentConnection();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                IRabbitMQPersistentConnection rabbitMQPersistentConnection = MateralServices.GetService<IRabbitMQPersistentConnection>();
                ILogger<EventBusRabbitMQ>? logger = serviceProvider.GetService<ILogger<EventBusRabbitMQ>>();
                IEventBusSubscriptionsManager eventBusSubscriptionsManager = MateralServices.GetService<IEventBusSubscriptionsManager>();
                return new EventBusRabbitMQ(rabbitMQPersistentConnection, serviceProvider, eventBusSubscriptionsManager, queueName, RCConfig.EventBusConfig.ExchangeName, false, logger);
            });
            return services;
        }
        public static IServiceCollection AddIntegrationEventHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type handlerType in assembly.ExportedTypes)
                {
                    if (handlerType.IsAbstract || !handlerType.IsAssignableTo(typeof(IIntegrationEventHandler))) continue;
                    services.AddTransient(handlerType);
                }
            }
            return services;
        }
    }
}
