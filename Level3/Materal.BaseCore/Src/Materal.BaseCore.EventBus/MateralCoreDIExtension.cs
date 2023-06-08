using Materal.Abstractions;
using Materal.BaseCore.Common;
using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Reflection;

namespace Materal.BaseCore.EventBus
{
    /// <summary>
    /// 发布中心依赖注入扩展
    /// </summary>
    public static class TFMSDIExtension
    {
        /// <summary>
        /// 自动订阅处理器
        /// </summary>
        private static List<Type>? subscribeHandlers = new();
        /// <summary>
        /// 添加Integration事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoSubscribe">自动订阅</param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName, params Assembly[] assemblies)
        {
            services.AddEventBus(queueName, true, assemblies);
            return services;
        }
        /// <summary>
        /// 添加Integration事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoSubscribe">自动订阅</param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName, bool autoSubscribe, params Assembly[] assemblies)
        {
            services.AddEventBus(queueName);
            services.AddEventHandlers(autoSubscribe, assemblies);
            return services;
        }
        /// <summary>
        /// 添加Integration事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName)
        {
            services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => new ConnectionFactory
            {
                HostName = MateralCoreConfig.EventBusConfig.HostName,
                Port = MateralCoreConfig.EventBusConfig.Port,
                DispatchConsumersAsync = true,
                UserName = MateralCoreConfig.EventBusConfig.UserName,
                Password = MateralCoreConfig.EventBusConfig.Password
            });
            MateralTFMSRabbitMQConfig.EventErrorConfig.Discard = MateralCoreConfig.EventBusConfig.DiscardErrorMessage;//消息处理失败后是否丢弃消息
            MateralTFMSRabbitMQConfig.EventErrorConfig.IsRetry = MateralCoreConfig.EventBusConfig.RetryNumber > 0;//消息处理失败后是否重试
            MateralTFMSRabbitMQConfig.EventErrorConfig.RetryNumber = MateralCoreConfig.EventBusConfig.RetryNumber;//消息处理失败后重试次数
            MateralTFMSRabbitMQConfig.EventErrorConfig.RetryInterval = TimeSpan.FromSeconds(MateralCoreConfig.EventBusConfig.RetryIntervalSecond);//消息处理失败后重试次数
            services.AddEventBusSubscriptionsManager().AddRabbitMQPersistentConnection();
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                IRabbitMQPersistentConnection rabbitMQPersistentConnection = MateralServices.GetService<IRabbitMQPersistentConnection>();
                ILogger<EventBusRabbitMQ>? logger = serviceProvider.GetService<ILogger<EventBusRabbitMQ>>();
                IEventBusSubscriptionsManager eventBusSubscriptionsManager = MateralServices.GetService<IEventBusSubscriptionsManager>();
                EventBusRabbitMQ eventBus = new(rabbitMQPersistentConnection, serviceProvider, eventBusSubscriptionsManager, queueName, MateralCoreConfig.EventBusConfig.ExchangeName, false, logger);
                if(subscribeHandlers != null)
                {
                    while (subscribeHandlers.Count > 0)
                    {
                        Type handler = subscribeHandlers.First();
                        Type? eventHanlerInterfaceType = handler.GetAllInterfaces().FirstOrDefault(m => m.IsGenericType && m.GenericTypeArguments.Length == 1 && m.IsAssignableTo<IIntegrationEventHandler>());
                        if (eventHanlerInterfaceType == null) continue;
                        Type eventType = eventHanlerInterfaceType.GenericTypeArguments.First();
                        if (!eventType.IsAssignableTo<IntegrationEvent>()) continue;
                        eventBus.SubscribeAsync(eventType, eventHanlerInterfaceType);
                        subscribeHandlers.RemoveAt(0);
                    }
                    subscribeHandlers = null;
                    eventBus.StartListening();
                }
                return eventBus;
            });
            return services;
        }
        /// <summary>
        /// 添加Integration处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoSubscribe"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddEventHandlers(true, assemblies);
            return services;
        }
        /// <summary>
        /// 添加Integration处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoSubscribe"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventHandlers(this IServiceCollection services, bool autoSubscribe, params Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type handlerType in assembly.ExportedTypes)
                {
                    if (handlerType.IsAbstract || !handlerType.IsAssignableTo(typeof(IIntegrationEventHandler))) continue;
                    services.AddTransient(handlerType);
                    if (autoSubscribe)
                    {
                        subscribeHandlers ??= new();
                        subscribeHandlers.Add(handlerType);
                    }
                }
            }
            return services;
        }
    }
}
