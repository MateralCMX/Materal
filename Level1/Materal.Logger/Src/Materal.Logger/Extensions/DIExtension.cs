﻿namespace Materal.Logger
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
        /// <param name="getLoggerLog"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfigOptions>? options = null, IConfiguration? configuration = null, Func<LoggerConfig, ILoggerLog>? getLoggerLog = null, params Assembly[] assemblies)
        {
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
            });
            services.Replace(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.Replace(ServiceDescriptor.Singleton<ILoggerProvider, LoggerProvider>());
            const string assembliyName = "Materal.Logger";
            List<Assembly> targetAssemblies = [.. assemblies];
            targetAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies().Where(m => m.FullName is not null && m.FullName.StartsWith(assembliyName)));
            FileInfo dllFileInfo = new(typeof(Logger).Assembly.Location);
            DirectoryInfo directoryInfo = dllFileInfo.Directory ?? throw new LoggerException("获取dll文件夹信息失败");
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.dll").Where(m => m.Name.StartsWith(assembliyName)).ToArray();
            foreach (FileInfo fileInfo in fileInfos)
            {
                if (targetAssemblies.Any(m => m.Location == fileInfo.FullName)) continue;
                Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                targetAssemblies.Add(assembly);
            }
            List<LoggerTargetConfigModel> configModels = [];
            foreach (Assembly assembly in targetAssemblies)
            {
                Type[] configTypes = assembly.GetTypes().Where(m => m.IsClass && !m.IsAbstract && m.IsAssignableTo<LoggerTargetConfigModel>()).ToArray();
                foreach (Type configType in configTypes)
                {
                    LoggerTargetConfigModel targetConfig = configType.Instantiation<LoggerTargetConfigModel>();
                    configModels.Add(targetConfig);
                }
            }
            services.TryAddSingleton(m => new LoggerConfig(configModels, options, configuration ?? m.GetService<IConfiguration>(), m));
            services.TryAddSingleton(m =>
            {
                LoggerConfig config = m.GetRequiredService<LoggerConfig>();
                return getLoggerLog is null ? new ConsoleLoggerLog(config) : getLoggerLog(config);
            });
            services.TryAddSingleton(m =>
            {
                LoggerConfig config = m.GetRequiredService<LoggerConfig>();
                ILoggerLog loggerLog = m.GetRequiredService<ILoggerLog>();
                LoggerRuntime loggerRuntime = new(m, config, loggerLog);
                loggerRuntime.AddHandlers(configModels);
                loggerRuntime.OnLoggerServiceReady();
                return loggerRuntime;
            });
            return services;
        }
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <param name="getLoggerLog"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, Action<LoggerConfigOptions> options, Func<LoggerConfig, ILoggerLog>? getLoggerLog, params Assembly[] assemblies)
            => AddMateralLogger(services, options, null, getLoggerLog, assemblies);
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="getLoggerLog"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration configuration, Action<LoggerConfigOptions> options, Func<LoggerConfig, ILoggerLog>? getLoggerLog = null, params Assembly[] assemblies)
            => AddMateralLogger(services, options, configuration, getLoggerLog, assemblies);
        /// <summary>
        /// 添加日志服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="getLoggerLog"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralLogger(this IServiceCollection services, IConfiguration configuration, Func<LoggerConfig, ILoggerLog>? getLoggerLog = null, params Assembly[] assemblies)
            => AddMateralLogger(services, null, configuration, getLoggerLog, assemblies);
    }
}
