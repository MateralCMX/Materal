#if NET
using Microsoft.Extensions.Hosting;

namespace Materal.Logger.Extensions
{
    /// <summary>
    /// 主机构建器扩展
    /// </summary>
    public static class HostApplicationBuilderExtensions
    {
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IHostApplicationBuilder AddMateralLogger(this IHostApplicationBuilder builder, IConfiguration? configuration, Action<LoggerOptions>? options, bool clearOtherProvider = false)
        {
            builder.Services.AddMateralLogger(configuration, options, clearOtherProvider);
            return builder;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IHostApplicationBuilder AddMateralLogger(this IHostApplicationBuilder builder, Action<LoggerOptions>? options, bool clearOtherProvider = false)
            => builder.AddMateralLogger(null, options, clearOtherProvider);
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IHostApplicationBuilder AddMateralLogger(this IHostApplicationBuilder builder, IConfiguration? configuration, bool clearOtherProvider = false)
            => builder.AddMateralLogger(configuration, null, clearOtherProvider);
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static IHostApplicationBuilder AddMateralLogger(this IHostApplicationBuilder builder, bool clearOtherProvider = false)
            => builder.AddMateralLogger(null, null, clearOtherProvider);
    }
}
#endif