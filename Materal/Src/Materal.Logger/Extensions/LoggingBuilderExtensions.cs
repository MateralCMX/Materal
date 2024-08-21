namespace Materal.Logger.Extensions
{
    /// <summary>
    /// 日志构建器扩展
    /// </summary>
    public static class LoggingBuilderExtensions
    {
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, IConfiguration? configuration, Action<LoggerOptions>? options, bool clearOtherProvider = false)
        {
            if (clearOtherProvider)
            {
                builder.ClearProviders();
            }
            builder.SetMinimumLevel(LogLevel.Trace);
            builder.Services.AddSingleton<ILoggerProvider, LoggerProvider>();
            builder.Services.AddSingleton<ILoggerInfo, DefaultLoggerInfo>();
            builder.Services.AddSingleton<ILoggerListener, LoggerListener>();
            LoggerProviderOptions.RegisterProviderOptions<LoggerOptions, LoggerProvider>(builder.Services);
            if (configuration is not null)
            {
                builder.Services.AddSingleton(configuration);
                builder.AddConfiguration(configuration);
                IConfigurationSection configurationSection = configuration.GetSection("Logging:MateralLogger");
                if (configurationSection is not null)
                {
                    builder.Services.Configure<LoggerOptions>(configurationSection);
                }
                else
                {
                    builder.Services.Configure<LoggerOptions>(configuration);
                }
            }
            else
            {
                builder.AddConfiguration();
            }
            if (options is not null)
            {
                builder.Services.Configure(options);
            }
            builder.Services.TryAddSingleton<ILoggerHost, LoggerHost>();
            AddLogWriter(builder.Services);
            return builder;
        }
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, Action<LoggerOptions>? options, bool clearOtherProvider = false)
            => builder.AddMateralLogger(null, options, clearOtherProvider);
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, IConfiguration? configuration, bool clearOtherProvider = false)
            => builder.AddMateralLogger(configuration, null, clearOtherProvider);
        /// <summary>
        /// 添加Materal日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="clearOtherProvider"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddMateralLogger(this ILoggingBuilder builder, bool clearOtherProvider = false)
            => builder.AddMateralLogger(null, null, clearOtherProvider);
        /// <summary>
        /// 添加日志写入器
        /// </summary>
        /// <param name="services"></param>
        private static void AddLogWriter(IServiceCollection services)
        {
            string libPath = typeof(Logger).Assembly.GetDirectoryPath();
            DirectoryInfo libDirectoryInfo = new(libPath);
            FileInfo[] fileInfos = libDirectoryInfo.GetFiles("*.dll");
            foreach (FileInfo fileInfo in fileInfos)
            {
                AddLogWriter(services, fileInfo);
            }
        }
        /// <summary>
        /// 添加日志写入器
        /// </summary>
        /// <param name="services"></param>
        /// <param name="fileInfo"></param>
        private static void AddLogWriter(IServiceCollection services, FileInfo fileInfo)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(fileInfo.FullName);
                MateralLoggerAssemblyAttribute? attribute = assembly.GetCustomAttribute<MateralLoggerAssemblyAttribute>();
                if (attribute is null) return;
                Type[] allTypes = assembly.GetTypes();
                IEnumerable<Type> loggerWriterTypes = allTypes.Where(m => m.IsClass && !m.IsAbstract && m.IsAssignableTo<ILoggerWriter>());
                IEnumerable<Type> loggerTargetOptionsTypes = allTypes.Where(m => m.IsClass && !m.IsAbstract && m.IsAssignableTo<LoggerTargetOptions>());
                foreach (Type loggerWriterType in loggerWriterTypes)
                {
                    string targetOptionsTypeName = $"{loggerWriterType.Name[..^6]}TargetOptions";
                    Type? loggerTargetOptionsType = loggerTargetOptionsTypes.FirstOrDefault(m => m.Name == targetOptionsTypeName);
                    if (loggerTargetOptionsType is null) return;
                    services.AddSingleton(typeof(ILoggerWriter), loggerWriterType);
                    LoggerOptions.LoggerTargetOptionTypes.Add(loggerTargetOptionsType);
                }
            }
            catch
            {
            }
        }
    }
}
