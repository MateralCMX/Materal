using Materal.Gateway.OcelotExtension.Custom;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;

namespace Materal.Gateway.OcelotExtension
{
    /// <summary>
    /// Ocelot构建器扩展
    /// </summary>
    public static partial class OcelotBuilderExtension
    {
        /// <summary>
        /// 添加自定义处理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddCustomHandler<T>(this IOcelotBuilder builder)
            where T : class, ICustomHandler
        {
            DefaultCustomHandlers.AddHandler<T>();
            builder.Services.AddSingleton<T>();
            return builder;
        }
        /// <summary>
        /// 添加自定义处理器
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="customHandler"></param>
        /// <returns></returns>
        public static IOcelotBuilder AddCustomHandler(this IOcelotBuilder builder, ICustomHandler customHandler)
        {
            DefaultCustomHandlers.AddHandler(customHandler.GetType());
            builder.Services.AddSingleton(customHandler);
            return builder;
        }
    }
}
