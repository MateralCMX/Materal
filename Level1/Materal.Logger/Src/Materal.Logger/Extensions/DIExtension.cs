using Materal.Logger.LoggerHandlers;
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
            DirectoryInfo directoryInfo = new(LoggerHandlerHelper.RootPath);
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.dll");
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
            return services;
        }
    }
}
