namespace Materal.EventBus.Abstraction
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, params Assembly[] handlerAssemblies)
        {
            services.TryAddSingleton<IEventBus, EventBusImpl>();
            services.AddEventBusHandlers(handlerAssemblies);
            return services;
        }
        /// <summary>
        /// 添加事件总线处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBusHandlers(this IServiceCollection services, params Assembly[] handlerAssemblies)
        {
            foreach (Assembly handlerAssembly in handlerAssemblies)
            {
                services.AddEventBusHandler(handlerAssembly);
            }
            return services;
        }
        /// <summary>
        /// 添加事件总线处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBusHandler(this IServiceCollection services, Assembly handlerAssembly)
        {
            foreach (Type eventHandlerType in handlerAssembly.GetTypes().Where(m => m.IsClass && !m.IsAbstract))
            {
                services.AddEventBusHandler(eventHandlerType);
            }
            return services;
        }
        /// <summary>
        /// 添加事件总线处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="eventHandlerType"></param>
        /// <returns></returns>
        /// <exception cref="EventBusException"></exception>
        public static IServiceCollection AddEventBusHandler(this IServiceCollection services, Type eventHandlerType)
        {
            if (eventHandlerType.IsAbstract || !eventHandlerType.IsClass) return services;
            foreach (Type interfaceType in eventHandlerType.GetInterfaces())
            {
                if (!interfaceType.IsGenericType || interfaceType.GetGenericTypeDefinition() != typeof(IEventHandler<>)) continue;
                Type eventType = interfaceType.GetGenericArguments()[0];
                services.AddEventBusHandler(eventType, eventHandlerType);
                return services;
            }
            return services;
        }
        /// <summary>
        /// 添加事件总线处理器
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBusHandler<TEvent, TEventHandler>(this IServiceCollection services)
            where TEvent : class, IEvent
            where TEventHandler : class, IEventHandler<TEvent>
        {
            services.AddEventBusHandler(typeof(TEvent), typeof(TEventHandler));
            return services;
        }
        /// <summary>
        /// 添加事件总线处理器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="eventType"></param>
        /// <param name="eventHandlerType"></param>
        /// <returns></returns>
        /// <exception cref="EventBusException"></exception>
        public static IServiceCollection AddEventBusHandler(this IServiceCollection services, Type eventType, Type eventHandlerType)
        {
            if (eventType.IsAbstract || !eventType.IsClass) return services;
            if (eventHandlerType.IsAbstract || !eventHandlerType.IsClass) return services;
            if (!eventType.IsAssignableTo<IEvent>()) throw new EventBusException($"{eventType.FullName}未实现{typeof(IEvent).FullName}接口");
            Type type = typeof(IEventHandler<>).MakeGenericType(eventType);
            if (!eventHandlerType.IsAssignableTo(type)) throw new EventBusException($"{eventHandlerType.FullName}未实现{type.FullName}接口");
            services.TryAddTransient(eventHandlerType);
            return services;
        }
    }
}
