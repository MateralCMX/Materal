using Materal.Logger.LoggerLogs;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <param name="clearOtherProvider"></param>
        /// <param name="getLoggerLog"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfig>? configure = null, bool clearOtherProvider = true, Func<ILoggerLog>? getLoggerLog = null)
        {
            services.AddLogging(bulider => bulider.AddMateralLogger(configure, clearOtherProvider, getLoggerLog));
            return services;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configure"></param>
        /// <param name="clearOtherProvider"></param>
        /// <param name="getLoggerLog"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration? configuration, Action<LoggerConfig>? configure = null, bool clearOtherProvider = true, Func<ILoggerLog>? getLoggerLog = null)
        {
            services.AddLogging(bulider => bulider.AddMateralLogger(configuration, configure, clearOtherProvider, getLoggerLog));
            return services;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfig> configure)
        {
            services.AddLogging(bulider => bulider.AddMateralLogger(configure));
            return services;
        }
    }
}
