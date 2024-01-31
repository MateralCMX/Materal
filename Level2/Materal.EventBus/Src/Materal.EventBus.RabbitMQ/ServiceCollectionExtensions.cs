namespace Materal.EventBus.RabbitMQ
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
        /// <param name="configuration"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration, params Assembly[] handlerAssemblies)
            => services.AddRabbitMQEventBus(true, configuration, null, handlerAssemblies);
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration, Action<EventBusConfig>? options, params Assembly[] handlerAssemblies)
            => services.AddRabbitMQEventBus(true, configuration, options, handlerAssemblies);
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoSubscribe"></param>
        /// <param name="configuration"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, bool autoSubscribe, IConfiguration configuration, params Assembly[] handlerAssemblies)
            => services.AddRabbitMQEventBus(autoSubscribe, configuration, null, handlerAssemblies);
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoSubscribe"></param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, bool autoSubscribe, IConfiguration configuration, Action<EventBusConfig>? options = null, params Assembly[] handlerAssemblies)
        {
            services.AddOptions();
            services.Configure<EventBusConfig>(configuration);
            if(options is not null)
            {
                services.Configure(options);
            }
            services.TryAddSingleton<IRabbitMQPersistentConnection, RabbitMQPersistentConnection>();
            services.TryAddSingleton<IEventBus, RabbitMQEventBusImpl>();
            services.AddEventBusHandlers(autoSubscribe, handlerAssemblies);
            return services;
        }
    }
}
