using Materal.Abstractions;
using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Materal.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Reflection;

namespace Materal.BaseCore.Common
{
    /// <summary>
    /// 发布中心依赖注入扩展
    /// </summary>
    public static class MateralCoreDIExtension
    {
        /// <summary>
        /// 添加发布中心服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoMapperAssemblys"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralCoreServices(this IServiceCollection services, params Assembly[] autoMapperAssemblys)
        {
            MateralConfig.PageStartNumber = 1;
            services.AddMemoryCache();
            services.AddMateralUtils();
            services.AddAutoMapper(config =>
            {
                config.AllowNullCollections = true;
            }, autoMapperAssemblys);
            return services;
        }
        /// <summary>
        /// 添加Integration事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public static IServiceCollection AddIntegrationEventBus(this IServiceCollection services, string queueName)
        {
            services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => new ConnectionFactory
            {
                HostName = MateralCoreConfig.EventBusConfig.HostName,
                Port = MateralCoreConfig.EventBusConfig.Port,
                DispatchConsumersAsync = true,
                UserName = MateralCoreConfig.EventBusConfig.UserName,
                Password = MateralCoreConfig.EventBusConfig.Password
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
                return new EventBusRabbitMQ(rabbitMQPersistentConnection, serviceProvider, eventBusSubscriptionsManager, queueName, MateralCoreConfig.EventBusConfig.ExchangeName, false, logger);
            });
            return services;
        }
        /// <summary>
        /// 添加Integration处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
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
