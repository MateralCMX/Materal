using System;
using System.Collections.Generic;
using System.Linq;

namespace Materal.TFMS.EventBus
{
    /// <inheritdoc />
    /// <summary>
    /// 内存事件总线订阅管理器
    /// </summary>
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        /// <summary>
        /// 处理器组
        /// </summary>
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        /// <summary>
        /// 事件类型
        /// </summary>
        private readonly List<Type> _eventTypes;
        public event EventHandler<string> OnEventRemoved;
        public bool IsEmpty => _handlers?.Count == 0;
        /// <summary>
        /// 内存事件总线订阅管理器
        /// </summary>
        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public void AddDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            DoAddSubscription(typeof(TH), eventName, true);
        }

        public void AddSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            string eventName = GetEventKey<T>(out Type eventType);
            DoAddSubscription(typeof(TH), eventName, false);
            if (!_eventTypes.Contains(eventType))
            {
                _eventTypes.Add(eventType);
            }
        }

        public void RemoveSubscription<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            SubscriptionInfo handlerToRemove = FindSubscription<T, TH>();
            string eventName = GetEventKey<T>();
            DoRemoveSubscription(eventName, handlerToRemove);
        }

        public void RemoveDynamicSubscription<TH>(string eventName) where TH : IDynamicIntegrationEventHandler
        {
            SubscriptionInfo handlerToRemove = FindDynamicSubscription<TH>(eventName);
            DoRemoveSubscription(eventName, handlerToRemove);
        }

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            string eventName = GetEventKey<T>();
            return HasSubscriptionsForEvent(eventName);
        }

        public bool HasSubscriptionsForEvent(string eventName)
        {
            return _handlers.ContainsKey(eventName);
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(m => m.Name == eventName);
        }

        public void Clear()
        {
            _handlers.Clear();
            _eventTypes.Clear();
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            string eventName = GetEventKey<T>();
            return GetHandlersForEvent(eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        #region 私有方法
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="handlerType">处理器类型</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="isDynamic">是否为动态</param>
        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            if (handlerType == null) throw new ArgumentNullException(nameof(handlerType));
            if (eventName == null) throw new ArgumentNullException(nameof(eventName));
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(m => m.HandlerType == handlerType))
            {
                throw new ArgumentException($"处理器{handlerType}已经注册给事件{eventName}", nameof(handlerType));
            }

            _handlers[eventName].Add(isDynamic
                ? SubscriptionInfo.GetDynamicEventSubscriptionInfo(handlerType)
                : SubscriptionInfo.GetEventSubscriptionInfo(handlerType));
        }
        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="subscriptionInfo"></param>
        private void DoRemoveSubscription(string eventName,SubscriptionInfo subscriptionInfo)
        {
            if (eventName == null) throw new ArgumentNullException(nameof(eventName));
            if (subscriptionInfo == null) throw new ArgumentNullException(nameof(subscriptionInfo));
            if (!_handlers.ContainsKey(eventName)) throw new ArgumentException($"未注册事件{eventName}", nameof(eventName));
            _handlers[eventName].Remove(subscriptionInfo);
            if (_handlers[eventName].Any()) return;
            _handlers.Remove(eventName);
            Type eventType = GetEventTypeByName(eventName);
            if (eventType != null) _eventTypes.Remove(eventType);
            OnEventRemoved?.Invoke(this, eventName);
        }
        /// <summary>
        /// 寻找动态订阅
        /// </summary>
        /// <typeparam name="TH"></typeparam>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private SubscriptionInfo FindDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            return DoFindSubscription(eventName, typeof(TH));
        }
        /// <summary>
        /// 寻找订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        /// <returns></returns>
        private SubscriptionInfo FindSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            string eventName = GetEventKey<T>();
            return DoFindSubscription(eventName, typeof(TH));
        }
        /// <summary>
        /// 寻找订阅
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="handlerType"></param>
        /// <returns></returns>
        private SubscriptionInfo DoFindSubscription(string eventName, Type handlerType)
        {
            if (HasSubscriptionsForEvent(eventName)) throw new ArgumentException($"事件{eventName}没有订阅", nameof(eventName));
            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
        /// <summary>
        /// 获得事件键
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetEventKey<T>(out Type type)
        {
            type = typeof(T);
            return type.Name;
        }
        #endregion
    }
}
