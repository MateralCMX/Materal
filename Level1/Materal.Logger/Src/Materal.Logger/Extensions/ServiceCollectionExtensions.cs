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
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfig>? configure = null, bool clearOtherProvider = true)
        {
            services.AddLogging(bulider => bulider.AddMateralLogger(configure, clearOtherProvider));
            return services;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="configure"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration? configuration, Action<LoggerConfig>? configure = null, bool clearOtherProvider = true)
        {
            services.AddLogging(bulider => bulider.AddMateralLogger(configuration, configure, clearOtherProvider));
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
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLoggerConfig(this IServiceCollection services, Action<LoggerConfig> configure)
        {
            services.AddLogging(bulider => bulider.AddMateralLoggerConfig(configure));
            return services;
        }
    }
}
