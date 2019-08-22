using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Materal.TFMS.EventBus.RabbitMQ.Extensions
{
    /// <summary>
    /// 事件总线扩展
    /// </summary>
    public static class RabbitMQEventBusExtensions
    {
        /// <summary>
        /// 添加RabbitMQ持久连接事件总线订阅
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQPersistentConnection(this IServiceCollection services)
        {
            return AddRabbitMQPersistentConnection<DefaultRabbitMQPersistentConnection>(services);
        }
        /// <summary>
        /// 添加RabbitMQ持久连接事件总线订阅
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRabbitMQPersistentConnection<T>(this IServiceCollection services) where T : class, IRabbitMQPersistentConnection
        {
            services.AddSingleton<IRabbitMQPersistentConnection, T>();
            return services;
        }
    }
}
