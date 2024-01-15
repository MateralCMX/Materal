namespace Materal.TFMS.EventBus
{
    /// <summary>
    /// 事件总线扩展
    /// </summary>
    public static class EventBusExtensions
    {
        /// <summary>
        /// 添加事件总线订阅
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBusSubscriptionsManager(this IServiceCollection services)
        {
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            return services;
        }
    }
}
