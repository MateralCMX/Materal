using Materal.Logger;
using Materal.Logger.LoggerLogs;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 日志构建器扩展
    /// </summary>
    public static class LoggingBuilderExtensions
    {
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <param name="clearOtherProvider"></param>
        /// <param name="getLoggerLog"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, Action<LoggerConfig>? configure = null, bool clearOtherProvider = true, Func<ILoggerLog>? getLoggerLog = null)
        {
            builder.AddMateralLogger(null, configure, clearOtherProvider, getLoggerLog);
            return builder;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <param name="configure"></param>
        /// <param name="clearOtherProvider"></param>
        /// <param name="getLoggerLog"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, IConfiguration? configuration, Action<LoggerConfig>? configure = null, bool clearOtherProvider = true, Func<ILoggerLog>? getLoggerLog = null)
        {
            getLoggerLog ??= () => new ConsoleLoggerLog();
            if (clearOtherProvider)
            {
                builder.ClearProviders();
            }
            builder.SetMinimumLevel(LogLevel.Trace);
            builder.AddConfiguration();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<LoggerConfig, LoggerProvider>(builder.Services);
            if (configuration is not null)
            {
                builder.Services.TryAddSingleton(configuration);
                builder.Services.Configure<LoggerConfig>(configuration.GetSection("Logging:MateralLogger"));
            }
            if (configure is not null)
            {
                builder.Services.Configure(configure);
            }
            LoggerHost.LoggerLog = getLoggerLog();
            return builder;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLoggerConfig(this ILoggingBuilder builder, Action<LoggerConfig> configure)
        {
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
