﻿namespace Materal.TFMS.EventBus
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event"></param>
        Task PublishAsync(IntegrationEvent @event);
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        Task SubscribeAsync<T, THandler>()
            where T : IntegrationEvent
            where THandler : IIntegrationEventHandler<T>;
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventHandlerType"></param>
        /// <returns></returns>
        Task SubscribeAsync(Type eventType, Type eventHandlerType);
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="THandler"></typeparam>
        void Unsubscribe<T, THandler>()
            where T : IntegrationEvent
            where THandler : IIntegrationEventHandler<T>;
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="eventHandlerType"></param>
        /// <returns></returns>
        void Unsubscribe(Type eventType, Type eventHandlerType);
        /// <summary>
        /// 订阅动态事件
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="eventName"></param>
        Task SubscribeDynamicAsync<THandler>(string eventName)
            where THandler : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 取消订阅动态事件
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="eventName"></param>
        void UnsubscribeDynamic<THandler>(string eventName)
            where THandler : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 开始监听
        /// </summary>
        void StartListening();
    }
}
