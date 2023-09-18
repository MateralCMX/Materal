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
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfigOptions>? options = null, IConfiguration? configuration = null, params Assembly[] assemblies)
        {
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
            });
            services.Replace(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.Replace(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
            LoggerConfig config = new(options, configuration);
            services.AddSingleton(config);
            //const string assembliyName = "Materal.Logger";
            //List<Assembly> targetAssemblies = assemblies.ToList();
            //targetAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(m => m.FullName is not null && m.FullName.StartsWith(assembliyName)));
            //FileInfo dllFileInfo = new(typeof(Logger).Assembly.Location);
            //DirectoryInfo directoryInfo = dllFileInfo.Directory;
            //FileInfo[] fileInfos = directoryInfo.GetFiles("*.dll").Where(m => m.Name.StartsWith(assembliyName)).ToArray();
            //foreach (FileInfo fileInfo in fileInfos)
            //{
            //    if (targetAssemblies.Any(m => m.Location == fileInfo.FullName)) continue;
            //    Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
            //    targetAssemblies.Add(assembly);
            //}
            //List<LoggerTargetConfigModel> configModels = new();
            //foreach (Assembly assembly in targetAssemblies)
            //{
            //    Type[] configTypes = assembly.GetTypes().Where(m => m.IsClass && !m.IsAbstract && m.IsAssignableTo<LoggerTargetConfigModel>()).ToArray();
            //    foreach (Type configType in configTypes)
            //    {
            //        LoggerTargetConfigModel config = configType.Instantiation<LoggerTargetConfigModel>();
            //        configModels.Add(config);
            //    }
            //}
            //LoggerConfig.Init(configModels, options, configuration);
            //foreach (LoggerTargetConfigModel configModel in configModels)
            //{
            //    configModel.OnLoggerServiceReady();
            //}
            return services;
        }
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <param name="assemblies"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration configuration, Action<LoggerConfigOptions> options, params Assembly[] assemblies)
            => AddMateralLogger(services, options, configuration, assemblies);
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
            => AddMateralLogger(services, null, configuration, assemblies);
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfigOptions> options, params Assembly[] assemblies)
            => AddMateralLogger(services, options, null, assemblies);
    }
}
