using Materal.Logger.LoggerLogs;

namespace Materal.Logger.Extensions
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
        /// <param name="configuration"></param>
        /// <param name="configure"></param>
        /// <param name="clearOtherProvider"></param>
        /// <param name="getLoggerLog"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, IConfiguration configuration, Action<LoggerConfig>? configure = null, bool clearOtherProvider = true, Func<ILoggerLog>? getLoggerLog = null)
        {
            getLoggerLog ??= () => new ConsoleLoggerLog();
            LoggerConfig.Configuration = configuration;
            if (clearOtherProvider)
            {
                builder.ClearProviders();
            }
            builder.SetMinimumLevel(LogLevel.Trace);
            builder.AddConfiguration();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<LoggerConfig, LoggerProvider>(builder.Services);
            configure ??= m => { };
            builder.Services.Configure(configure);
            LoggerHost.LoggerLog = getLoggerLog();
            return builder;
        }
    }
}
