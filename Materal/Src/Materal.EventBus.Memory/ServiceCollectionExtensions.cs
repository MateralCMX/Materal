using Materal.EventBus.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Materal.EventBus.Memory
{
    /// <summary>
    /// ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryEventBus(this IServiceCollection services)
            => services.AddMemoryEventBus(false);
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryEventBus(this IServiceCollection services, params Assembly[] handlerAssemblies)
            => services.AddMemoryEventBus(true, handlerAssemblies);
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoSubscribe">自动订阅标识</param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryEventBus(this IServiceCollection services, bool autoSubscribe, params Assembly[] handlerAssemblies)
            => services.AddEventBus<MemoryEventBus>(autoSubscribe, handlerAssemblies);
    }
}
