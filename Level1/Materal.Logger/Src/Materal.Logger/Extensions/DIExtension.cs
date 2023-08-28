using Materal.Logger.LoggerHandlers;
using Materal.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System.Reflection;

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
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfigOptions>? options = null, IConfiguration? configuration = null)
        {
            services.AddMateralUtils();
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddFilter<LoggerProvider>(null, LogLevel.Trace);
            });
            services.Replace(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.Replace(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
            DirectoryInfo directoryInfo = new(LoggerHandlerHelper.RootPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.dll").Where(m => m.Name.StartsWith("Materal.Logger")).ToArray();
            Type loggerHandlerType = typeof(ILoggerHandler);
            foreach (FileInfo fileInfo in fileInfos)
            {
                Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                Type[] loggerHandlerTypes = assembly.GetTypes().Where(m => m.IsClass && !m.IsAbstract && m.IsAssignableTo<ILoggerHandler>()).ToArray();
                foreach (Type type in loggerHandlerTypes)
                {
                    services.AddSingleton(loggerHandlerType, type);
                }
            }
            LoggerConfig.Init(options, configuration);
            return services;
        }
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration configuration, Action<LoggerConfigOptions> options) 
            => AddMateralLogger(services, options, configuration);
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration configuration)
            => AddMateralLogger(services, null, configuration);
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfigOptions> options)
            => AddMateralLogger(services, options, null);
    }
}
