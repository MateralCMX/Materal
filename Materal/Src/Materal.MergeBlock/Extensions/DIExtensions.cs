using System.Runtime.Loader;

namespace Materal.MergeBlock.Extensions
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DIExtensions
    {
        /// <summary>
        /// 添加MergeBlock
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostApplicationBuilder AddMergeBlockCore(this IHostApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), builder.Configuration));
            services.Replace(ServiceDescriptor.Singleton(builder.Services));
            services.AddMergeBlockCore();
            return builder;
        }
        /// <summary>
        /// 添加MergeBlock
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        private static IServiceCollection AddMergeBlockCore(this IServiceCollection services)
        {
            MateralServices.Services = services;
            services.AddOptions();
            services.AddLogging();
            services.AddLocalization();
            services.AddMergeBlockLoggerFactory();
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
            PluginManager pluginManager = new();
            pluginManager.LoadModules();
            MergeBlockContext implementationInstance = new()
            {
                ModuleDescriptors = ModuleLoader.ModuleDescriptors,
                Plugins = pluginManager.Plugins.ToList<IPluginBase>(),
                MergeBlockAssemblies = pluginManager.Plugins.SelectMany(p => p.Assemblies).ToList()
            };
            services.AddSingleton(implementationInstance);
            services.AddSingleton<AdvancedContext>();
            ConfigModules(services);
            return services;
        }
        /// <summary>
        /// 配置模块
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigModules(IServiceCollection services)
        {
            IConfiguration? configuration = services.GetSingletonInstance<IConfiguration>();
            ServiceConfigurationContext context = new(services, configuration);
            ModuleLoader.ModuleDescriptors.ForEach(async m =>
            {
                m.Instance.OnPreConfigureServices(context);
                await m.Instance.OnPreConfigureServicesAsync(context);
            });
            ModuleLoader.ModuleDescriptors.ForEach(async m =>
            {
                m.Instance.OnConfigureServices(context);
                await m.Instance.OnConfigureServicesAsync(context);
            });
            ModuleLoader.ModuleDescriptors.ForEach(async m =>
            {
                m.Instance.OnPostConfigureServices(context);
                await m.Instance.OnPostConfigureServicesAsync(context);
            });
        }
        /// <summary>
        /// 添加MergeBlock日志工厂
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="MergeBlockException"></exception>
        private static IServiceCollection AddMergeBlockLoggerFactory(this IServiceCollection services)
        {
            IConfiguration config = services.GetSingletonInstance<IConfiguration>() ?? throw new MergeBlockException("未找到配置对象");
            ILoggerFactory loggerFactory = LoggerFactory.Create(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                IConfigurationSection LoggingConfig = config.GetSection("Logging");
                loggingBuilder.AddConfiguration(LoggingConfig);
            });
            MateralServices.Logger = loggerFactory.CreateLogger("MergeBlock");
            ObjectAccessor<ILoggerFactory> loggerFactoryInstance = new(loggerFactory);
            services.TryAddSingleton(loggerFactoryInstance);
            return services;
        }
        /// <summary>
        /// 默认解析
        /// </summary>
        /// <param name="context"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private static Assembly? Default_Resolving(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            Assembly? assembly = AssemblyLoadContext.Default.Assemblies.FirstOrDefault(m => m.GetName().Name == assemblyName.Name);
            if (assembly != null) return assembly;
            string str = Path.Combine(AppContext.BaseDirectory, $"{assemblyName.Name}.dll");
            if (!File.Exists(str)) return null;
            MateralServices.Logger?.LogDebug($"从主环境加载{assemblyName.Name}");
            return AssemblyLoadContext.Default.LoadFromAssemblyPath(str);
        }
        /// <summary>
        /// 使用MergeBlock
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost UseMergeBlock(this IHost host)
        {
            MateralServices.ServiceProvider = host.Services;
            AdvancedContext advancedContext = host.Services.GetRequiredService<AdvancedContext>();
            advancedContext.App = host;
            ApplicationInitializationContext context = new(host);
            InitMergeBlock(context);
            return host;
        }
        /// <summary>
        /// 初始化MergeBlock
        /// </summary>
        /// <param name="context"></param>
        private static void InitMergeBlock(ApplicationInitializationContext context)
        {
            ModuleLoader.ModuleDescriptors.ForEach(async m =>
            {
                m.Instance.OnPreApplicationInitialization(context);
                await m.Instance.OnPreApplicationInitializationAsync(context);
            });
            ModuleLoader.ModuleDescriptors.ForEach(async m =>
            {
                m.Instance.OnApplicationInitialization(context);
                await m.Instance.OnApplicationInitializationAsync(context);
            });
            ModuleLoader.ModuleDescriptors.ForEach(async m =>
            {
                m.Instance.OnPostApplicationInitialization(context);
                await m.Instance.OnPostApplicationInitializationAsync(context);
            });
        }
    }
}
