using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class LoggerDIExtension
    {
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddFilter<LoggerProvider>(null, LogLevel.Trace);
            });
            services.Replace(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.Replace(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
            return services;
        }
        /// <summary>
        /// 使用日志服务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="option"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMateralLogger(this IApplicationBuilder app, Action<LoggerConfigOptions>? option = null, IConfiguration? configuration = null)
        {
            LoggerManager.Init(option, configuration);
            return app;
        }
    }
}
