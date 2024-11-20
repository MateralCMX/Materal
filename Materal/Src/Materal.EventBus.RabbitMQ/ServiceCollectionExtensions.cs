using Materal.Utils.Extensions;

namespace Materal.EventBus.RabbitMQ
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
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration)
            => services.AddRabbitMQEventBus(false, configuration, null);
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
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration, Action<RabbitMQEventBusOptions>? options)
            => services.AddRabbitMQEventBus(false, configuration, options);
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, IConfiguration configuration, Action<RabbitMQEventBusOptions>? options, params Assembly[] handlerAssemblies)
            => services.AddRabbitMQEventBus(true, configuration, options, handlerAssemblies);
        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoSubscribe">自动订阅标识</param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="handlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, bool autoSubscribe, IConfiguration configuration, Action<RabbitMQEventBusOptions>? options, params Assembly[] handlerAssemblies)
        {
            services.AddOptions();
            services.Configure<RabbitMQEventBusOptions>(configuration);
            services.AddMateralUtils();
            if (options is not null)
            {
                services.Configure(options);
            }
            return services.AddEventBus<RabbitMQEventBus>(autoSubscribe, handlerAssemblies);
        }
    }
}
