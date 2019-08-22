using System;
using System.Collections.Generic;

namespace Materal.TFMS.EventBus
{
    /// <summary>
    /// 事件总线订阅管理器
    /// </summary>
    public interface IEventBusSubscriptionsManager
    {
        /// <summary>
        /// 是空的
        /// </summary>
        bool IsEmpty { get; }
        /// <summary>
        /// 事件移除事件
        /// </summary>
        event EventHandler<string> OnEventRemoved;
        /// <summary>
        /// 添加动态订阅
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void AddDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
        /// <summary>
        /// 移除动态订阅
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;
        /// <summary>
        /// 事件是否有订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        /// <summary>
        /// 根据事件名称查看事件是否有订阅
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        bool HasSubscriptionsForEvent(string eventName);
        /// <summary>
        /// 根据名称获得事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        Type GetEventTypeByName(string eventName);
        /// <summary>
        /// 清空
        /// </summary>
        void Clear();
        /// <summary>
        /// 根据事件获事件处理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        /// <summary>
        /// 根据事件名称获取事件处理器
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        /// <summary>
        /// 获得事件名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string GetEventKey<T>();
    }
}
