﻿using Materal.MergeBlock.EventBus;
using Materal.TFMS.EventBus;
using Materal.TFMS.EventBus.Extensions;
using Materal.TFMS.EventBus.RabbitMQ;
using Materal.TFMS.EventBus.RabbitMQ.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Materal.MergeBlock.Logger
{
    public class EventBusModule : MergeBlockModule, IMergeBlockModule
    {
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            List<Type> handlerTypes = GetAllEventHandler(context.ModuleInfos);
            foreach (Type handlerType in handlerTypes)
            {
                Type? eventHanlerInterfaceType = handlerType.GetAllInterfaces().FirstOrDefault(m => m.IsGenericType && m.GenericTypeArguments.Length == 1 && m.IsAssignableTo<IIntegrationEventHandler>());
                if (eventHanlerInterfaceType is null) return;
                context.Services.AddTransient(handlerType);
            }
            await base.OnConfigServiceAsync(context);
        }
        public override async Task OnConfigServiceAfterAsync(IConfigServiceContext context)
        {
            EventBusConfigModel eventBusConfig = context.Configuration.GetValueObject<EventBusConfigModel>(EventBusConfigModel.ConfigKey) ?? throw new MergeBlockException($"未找到EventBus配置[{EventBusConfigModel.ConfigKey}]");
            context.Services.AddTransient<IConnectionFactory, ConnectionFactory>(serviceProvider => new ConnectionFactory
            {
                HostName = eventBusConfig.HostName,
                Port = eventBusConfig.Port,
                DispatchConsumersAsync = true,
                UserName = eventBusConfig.UserName,
                Password = eventBusConfig.Password
            });
            MateralTFMSRabbitMQConfig.EventErrorConfig.Discard = eventBusConfig.DiscardErrorMessage;//消息处理失败后是否丢弃消息
            MateralTFMSRabbitMQConfig.EventErrorConfig.IsRetry = eventBusConfig.RetryNumber > 0;//消息处理失败后是否重试
            MateralTFMSRabbitMQConfig.EventErrorConfig.RetryNumber = eventBusConfig.RetryNumber;//消息处理失败后重试次数
            MateralTFMSRabbitMQConfig.EventErrorConfig.RetryInterval = TimeSpan.FromSeconds(eventBusConfig.RetryIntervalSecond);//消息处理失败后重试次数
            context.Services.AddEventBusSubscriptionsManager().AddRabbitMQPersistentConnection();
            context.Services.AddSingleton<IEventBus, EventBusRabbitMQ>(serviceProvider =>
            {
                IRabbitMQPersistentConnection rabbitMQPersistentConnection = serviceProvider.GetRequiredService<IRabbitMQPersistentConnection>();
                ILogger<EventBusRabbitMQ>? logger = serviceProvider.GetService<ILogger<EventBusRabbitMQ>>();
                IEventBusSubscriptionsManager eventBusSubscriptionsManager = serviceProvider.GetRequiredService<IEventBusSubscriptionsManager>();
                EventBusRabbitMQ eventBus = new(rabbitMQPersistentConnection, serviceProvider, eventBusSubscriptionsManager, eventBusConfig.QueueName, eventBusConfig.ExchangeName, false, logger);
                IEnumerable<IMergeBlockModuleInfo> moduleInfos = serviceProvider.GetServices<IMergeBlockModuleInfo>();
                List<Type> handlerTypes = GetAllEventHandler(context.ModuleInfos);
                foreach (Type handlerType in handlerTypes)
                {
                    Type? eventHanlerInterfaceType = handlerType.GetAllInterfaces().FirstOrDefault(m => m.IsGenericType && m.GenericTypeArguments.Length == 1 && m.IsAssignableTo<IIntegrationEventHandler>());
                    if (eventHanlerInterfaceType is not null)
                    {
                        Type eventType = eventHanlerInterfaceType.GenericTypeArguments.First();
                        if (eventType.IsAssignableTo<IntegrationEvent>())
                        {
                            eventBus.SubscribeAsync(eventType, handlerType).Wait();
                        }
                    }
                }
                return eventBus;
            });
            await base.OnConfigServiceAsync(context);
        }
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            IEventBus eventBus = context.ServiceProvider.GetRequiredService<IEventBus>();
            eventBus.StartListening();
            await base.OnApplicationInitAsync(context);
        }
        private static List<Type> GetAllEventHandler(IEnumerable<IMergeBlockModuleInfo> moduleInfos)
        {
            List<Type> result = [];
            foreach (IMergeBlockModuleInfo moduleInfo in moduleInfos)
            {
                IEnumerable<Type> types = moduleInfo.ModuleAssembly.GetTypes().Where(m => !m.IsAbstract && m.IsClass && m.IsAssignableTo<IIntegrationEventHandler>());
                result.AddRange(types);
            }
            return result;
        }
    }
}
