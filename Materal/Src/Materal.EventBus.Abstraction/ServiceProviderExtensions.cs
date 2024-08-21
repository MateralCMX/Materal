namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 服务提供者扩展
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// 使用事件总线
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceProvider UseEventBus(this IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<IEventBus>();
            return serviceProvider;
        }
    }
}
