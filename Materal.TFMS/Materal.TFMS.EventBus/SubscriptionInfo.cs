using System;

namespace Materal.TFMS.EventBus
{
    /// <summary>
    /// 订阅信息
    /// </summary>
    public class SubscriptionInfo
    {
        /// <summary>
        /// 是动态的
        /// </summary>
        public bool IsDynamic { get; }
        /// <summary>
        /// 处理器类型
        /// </summary>
        public Type HandlerType { get; }
        /// <summary>
        /// 订阅信息
        /// </summary>
        /// <param name="isDynamic">是动态的</param>
        /// <param name="handlerType">处理器类型</param>
        private SubscriptionInfo(bool isDynamic, Type handlerType)
        {
            IsDynamic = isDynamic;
            HandlerType = handlerType;
        }
        /// <summary>
        /// 获得动态订阅信息
        /// </summary>
        /// <param name="handlerType">处理器类型</param>
        /// <returns></returns>
        public static SubscriptionInfo GetDynamicEventSubscriptionInfo(Type handlerType)
        {
            return new SubscriptionInfo(true, handlerType);
        }
        /// <summary>
        /// 获得订阅信息
        /// </summary>
        /// <param name="handlerType"></param>
        /// <returns></returns>
        public static SubscriptionInfo GetEventSubscriptionInfo(Type handlerType)
        {
            return new SubscriptionInfo(false, handlerType);
        }
    }
}
