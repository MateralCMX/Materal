using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace Materal.Logger
{
    /// <summary>
    /// Materal日志记录器扩展
    /// </summary>
    public static class MateralLoggerExtensions
    {
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, bool clearOtherProvider = true)
        {
            if (clearOtherProvider)
            {
                builder.ClearProviders();
            }
            builder.AddConfiguration();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ConsoleLoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<MateralLoggerConfig, ConsoleLoggerProvider>(builder.Services);
            return builder;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, Action<MateralLoggerConfig> configure, bool clearOtherProvider = true)
        {
            builder.AddMateralLogger(clearOtherProvider);
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
