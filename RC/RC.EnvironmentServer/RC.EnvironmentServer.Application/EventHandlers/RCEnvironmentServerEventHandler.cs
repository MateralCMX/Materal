﻿using Materal.EventBus.Abstraction;
using Materal.EventBus.RabbitMQ;

namespace RC.EnvironmentServer.Application.EventHandlers
{
    /// <summary>
    /// RCEnvironmentServer事件处理器
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    [QueueName("RCEnvironmentServer")]
    public abstract class RCEnvironmentServerEventHandler<TEvent>(IOptionsMonitor<ApplicationConfig> applicationConfig, IOptionsMonitor<RabbitMQEventBusOptions> eventBusConfig) : BaseEventHandler<TEvent>, IEventHandler<TEvent>, IRabbitMQEventHandler
        where TEvent : IEvent
    {
        /// <summary>
        /// 应用程序配置
        /// </summary>
        protected readonly IOptionsMonitor<ApplicationConfig> AppConfig = applicationConfig;
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName => $"{AppConfig.CurrentValue.ServiceName}Queue{eventBusConfig.CurrentValue.NameSuffix}";
    }
}
