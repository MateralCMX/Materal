using Materal.Logger.LoggerHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Materal.Logger
{
    /// <summary>
    /// DI扩展
    /// </summary>
    public static class DIExtension
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
            services.AddSingleton<ILoggerHandler, ConsoleLoggerHandler>();
            return services;
        }
    }
}
