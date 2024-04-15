using Microsoft.Extensions.Hosting;

namespace Materal.Logger.Extensions
{
    /// <summary>
    /// 主机构建器扩展
    /// </summary>
    public static class HostApplicationBuilderExtension
    {
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IHostApplicationBuilder AddMateralLogger(this IHostApplicationBuilder builder, Action<LoggerConfig>? configure = null, bool clearOtherProvider = true)
        {
            builder.Logging.AddMateralLogger(configure, clearOtherProvider);
            return builder;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IHostApplicationBuilder AddMateralLoggerConfig(this IHostApplicationBuilder builder, Action<LoggerConfig> configure)
        {
            builder.Logging.AddMateralLoggerConfig(configure);
            return builder;
        }
    }
}
