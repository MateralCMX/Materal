namespace Materal.Logger.Extensions
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
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration? configuration, Action<LoggerOptions>? options, bool clearOtherProvider = false)
        {
            services.AddLogging(bulider => bulider.AddMateralLogger(configuration, options, clearOtherProvider));
            return services;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerOptions>? options, bool clearOtherProvider = false)
            => services.AddMateralLogger(null, options, clearOtherProvider);
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration? configuration,  bool clearOtherProvider = false)
            => services.AddMateralLogger(configuration, null, clearOtherProvider);
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="services"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, bool clearOtherProvider = false)
            => services.AddMateralLogger(null, null, clearOtherProvider);
    }
}
