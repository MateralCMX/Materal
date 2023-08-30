using Materal.Logger.LoggerHandlers;
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
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
            });
            services.Replace(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.Replace(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
            const string assembliyName = "Materal.Logger";
            List<Assembly> assemblies = new();
            assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(m => m.FullName is not null && m.FullName.StartsWith(assembliyName)));
            FileInfo dllFileInfo = new(typeof(Logger).Assembly.Location);
            DirectoryInfo directoryInfo = dllFileInfo.Directory;
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.dll").Where(m => m.Name.StartsWith(assembliyName)).ToArray();
            foreach (FileInfo fileInfo in fileInfos)
            {
                if (assemblies.Any(m => m.Location == fileInfo.FullName)) continue;
                Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                assemblies.Add(assembly);
            }
            foreach (Assembly assembly in assemblies)
            {
                Type[] loggerHandlerTypes = assembly.GetTypes().Where(m => m.IsClass && !m.IsAbstract && m.IsAssignableTo<ILoggerHandler>()).ToArray();
                foreach (Type type in loggerHandlerTypes)
                {
                    object loggerHandlerObj = type.Instantiation();
                    if (loggerHandlerObj is not ILoggerHandler loggerHandler) continue;
                    Logger.Handlers.Add(loggerHandler);
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
