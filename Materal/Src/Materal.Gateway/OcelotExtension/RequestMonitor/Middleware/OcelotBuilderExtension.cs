using Materal.Gateway.OcelotExtension.RequestMonitor;
using Ocelot.DependencyInjection;

namespace Materal.Gateway.OcelotExtension
{
    /// <summary>
    /// Ocelot构建器扩展
    /// </summary>
    public static partial class OcelotBuilderExtension
    {
        /// <summary>
        /// 添加请求监控
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddRequestMonitor<T>(this IOcelotBuilder builder)
            where T : class, IRequestMonitorHandler
        {
            DefatultRequestMonitorHandlers.AddHandler<T>();
            builder.Services.AddSingleton<T>();
            return builder;
        }
        /// <summary>
        /// 添加请求监控
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="customHandler"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddRequestMonitor(this IOcelotBuilder builder, IRequestMonitorHandler customHandler)
        {
            DefatultRequestMonitorHandlers.AddHandler(customHandler.GetType());
            builder.Services.AddSingleton(customHandler);
            return builder;
        }
    }
}
