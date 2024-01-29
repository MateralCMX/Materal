using Materal.MergeBlock.ConsoleModule;
using Materal.MergeBlock.NormalModule;
using Materal.MergeBlock.WebModule;

namespace Materal.MergeBlock
{
    /// <summary>
    /// MergeBlock程序
    /// </summary>
    public abstract class MergeBlockProgram<TModule, TModuleInfo, TConfigServiceContext, TApplicationContext> : IMergeBlockProgram
        where TModule : IMergeBlockModule
        where TModuleInfo : IModuleInfo<TModule>
        where TConfigServiceContext : IConfigServiceContext
        where TApplicationContext : IApplicationContext
    {
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="args"></param>
        /// <param name="autoRemoveAssemblies"></param>
        /// <returns></returns>
        public abstract Task RunAsync(string[] args, bool autoRemoveAssemblies = true);
        /// <summary>
        /// 配置模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="autoRemoveAssemblies"></param>
        /// <returns></returns>
        public virtual async Task ConfigModuleAsync(IServiceCollection services, ConfigurationManager configuration, bool autoRemoveAssemblies)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IModuleBuilder, ConsoleModuleBuilder>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IModuleBuilder, WebModuleBuilder>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IModuleBuilder, NormalModuleBuilder>());
            services.AddOptions();
            services.Configure<MergeBlockConfig>(configuration);
            services.AddMateralUtils();
            TConfigServiceContext context = GetConfigServiceContext();
            LoadModules(context.ServiceProvider, autoRemoveAssemblies);
            List<IModuleInfo> allMergeBlockModules = MergeBlockHost.ModuleInfos;
            MergeBlockHost.Logger?.LogDebug($"找到{allMergeBlockModules.Count}个模块");
            List<IModuleInfo> tempMergeBlockModules = [];
            List<string> allMergeBlockModuleNames = [];
            foreach (IModuleInfo module in allMergeBlockModules)
            {
                IModuleInfo? duplicateModule = tempMergeBlockModules.FirstOrDefault(m => m.Name == module.Name);
                if (duplicateModule is not null) throw new MergeBlockException($"模块[{module.Location},{module.Name}|{module.Description}]与模块[{duplicateModule.Location},{duplicateModule.Name}|{module.Description}]重复");
                tempMergeBlockModules.Add(module);
                allMergeBlockModuleNames.Add(module.Name);
            }
            await RunModuleAsync(async m =>
            {
                foreach (string depend in m.Depends)
                {
                    if (allMergeBlockModuleNames.Contains(depend)) continue;
                    throw new MergeBlockException($"未找到模块{m.Name}的依赖模块{depend}");
                }
                await Task.CompletedTask;
            }, false);
            #region 配置服务之前
            await ConfigServiceBeforeAsync(context);
            await RunModuleAsync(async m => await m.ConfigServiceBeforeAsync(context));
            #endregion
            #region 配置服务
            List<Assembly> autoMapperAssemblyList = [];
            List<Assembly> processedAssemblies = [];
            #region 模块
            await ConfigServiceAsync(context);
            await RunModuleAsync(async m =>
            {
                await m.ConfigServiceAsync(context);
                if(processedAssemblies.Contains(m.ModuleType.Assembly)) return;
                Type[] allTypes = m.ModuleType.Assembly.GetTypes();
                #region AutoMapper
                if (!allTypes.Any(m => m.IsSubclassOf(typeof(Profile))))
                {
                    autoMapperAssemblyList.Add(m.ModuleType.Assembly);
                }
                #endregion
                #region 自动DI
                List<Type> autoDITypes = allTypes.Where(m => m.IsClass && !m.IsAbstract && m.IsAssignableTo<IDependencyInjectionService>()).ToList();
                foreach (Type type in autoDITypes)
                {
                    AddAutoDI(type, services);
                }
                #endregion
                processedAssemblies.Add(m.ModuleType.Assembly);
            });
            #endregion
            services.AddAutoMapper(config => config.AllowNullCollections = true, autoMapperAssemblyList);
            #endregion
            #region 配置服务之后
            await ConfigServiceAfterAsync(context);
            await RunModuleAsync(async m => await m.ConfigServiceAfterAsync(context));
            #endregion
        }
        private static void RemoveDuplicateAssemblies()
        {
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(m => m.FullName?.StartsWith("Materal.MergeBlock") ?? false).ToList();
            foreach (Assembly assembly in assemblies)
            {
                AppDomain.CurrentDomain.Load(assembly.GetName());
            }
        }
        /// <summary>
        /// 添加自动DI
        /// </summary>
        /// <param name="type"></param>
        /// <param name="services"></param>
        private static void AddAutoDI(Type type, IServiceCollection services)
        {
            List<Type> allInterfaces = type.GetInterfaces().Where(m => m.IsAssignableTo<IDependencyInjectionService>()).ToList();
            if (allInterfaces.Any(m => m.IsGenericType))
            {
                allInterfaces = allInterfaces.Where(m => m.IsGenericType).ToList();
            }
            DependencyInjectionAttribute dependencyInjectionAttribute = type.GetCustomAttribute<DependencyInjectionAttribute>() ?? new DependencyInjectionAttribute();
            foreach (Type interfaceType in allInterfaces)
            {
                AddAutoDI(type, interfaceType, dependencyInjectionAttribute, services);
            }
        }
        /// <summary>
        /// 添加自动DI
        /// </summary>
        /// <param name="type"></param>
        /// <param name="interfaceType"></param>
        /// <param name="attribute"></param>
        /// <param name="services"></param>
        private static void AddAutoDI(Type type, Type interfaceType, DependencyInjectionAttribute attribute, IServiceCollection services)
        {
            if (!interfaceType.IsAssignableTo<IDependencyInjectionService>()) return;
            Type? serviceType = attribute.ServiceType;
            if (serviceType is null)
            {
                if (interfaceType.IsGenericType)
                {
                    serviceType = interfaceType.GetGenericArguments().First();
                }
                else
                {
                    serviceType = type;
                }
            }
            if (serviceType is null) return;
            ServiceLifetime serviceLifetime = attribute.ServiceLifetime;
            if (interfaceType.IsAssignableTo<ISingletonDependencyInjectionService>())
            {
                serviceLifetime = ServiceLifetime.Singleton;
            }
            else if(interfaceType.IsAssignableTo<ITransientDependencyInjectionService>())
            {
                serviceLifetime = ServiceLifetime.Transient;
            }
            else if(interfaceType.IsAssignableTo<IScopedDependencyInjectionService>())
            {
                serviceLifetime = ServiceLifetime.Scoped;
            }
            AddAutoDI(type, serviceType, serviceLifetime, attribute.Type, services);
        }
        /// <summary>
        /// 添加自动DI
        /// </summary>
        /// <param name="type"></param>
        /// <param name="serviceType"></param>
        /// <param name="serviceLifetime"></param>
        /// <param name="dependencyInjectionType"></param>
        /// <param name="services"></param>
        private static void AddAutoDI(Type type, Type serviceType, ServiceLifetime serviceLifetime, DependencyInjectionTypeEnum dependencyInjectionType, IServiceCollection services)
        {
            ServiceDescriptor serviceDescriptor = type == serviceType ? new ServiceDescriptor(type, serviceLifetime) : new ServiceDescriptor(serviceType, type, serviceLifetime);
            switch (dependencyInjectionType)
            {
                case DependencyInjectionTypeEnum.TryAdd:
                    services.TryAdd(serviceDescriptor);
                    break;
                case DependencyInjectionTypeEnum.Add:
                    services.Add(serviceDescriptor);
                    break;
                case DependencyInjectionTypeEnum.Replace:
                    services.Replace(serviceDescriptor);
                    break;
            }
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected abstract TConfigServiceContext GetConfigServiceContext();
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task ConfigServiceBeforeAsync(TConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task ConfigServiceAsync(TConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task ConfigServiceAfterAsync(TConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 初始化模块
        /// </summary>
        /// <returns></returns>
        public virtual async Task InitModuleAsync(IServiceProvider serviceProvider)
        {
            TApplicationContext context = GetApplicationContext();
            await ApplicationInitBeforeAsync(context);
            await RunModuleAsync(async m => await m.ApplicationInitBeforeAsync(context));
            await ApplicationInitAsync(context);
            await RunModuleAsync(async m =>
            {
                MergeBlockHost.Logger?.LogDebug($"初始化模块[{m.Name}|{m.Description}]");
                await m.ApplicationInitAsync(context);
            });
            await ApplicationInitAfterAsync(context);
            await RunModuleAsync(async m => await m.ApplicationInitAfterAsync(context));
        }
        /// <summary>
        /// 获得配置服务上下文
        /// </summary>
        /// <returns></returns>
        protected abstract TApplicationContext GetApplicationContext();
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task ApplicationInitBeforeAsync(TApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task ApplicationInitAsync(TApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual async Task ApplicationInitAfterAsync(TApplicationContext context) => await Task.CompletedTask;
        #region 私有方法
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="autoRemoveAssemblies"></param>
        /// <returns></returns>
        private static void LoadModules(IServiceProvider serviceProvider, bool autoRemoveAssemblies)
        {
            IEnumerable<IModuleBuilder> moduleBuilders = serviceProvider.GetServices<IModuleBuilder>();
            IOptionsMonitor<MergeBlockConfig> mergeBlockConfig = serviceProvider.GetRequiredService<IOptionsMonitor<MergeBlockConfig>>();
            string rootModulesPath = AppDomain.CurrentDomain.BaseDirectory;
            LoadModules(moduleBuilders, new DirectoryInfo(rootModulesPath), false);
            string defaultModulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");
            string[] modulesDirectoryPaths = [defaultModulesPath, .. mergeBlockConfig.CurrentValue.ModulesDirectories];
            foreach (string moduleDirectoryPath in modulesDirectoryPaths)
            {
                DirectoryInfo modulesDirectoryInfo = new(moduleDirectoryPath);
                if (!modulesDirectoryInfo.Exists) continue;
                foreach (DirectoryInfo moduleDirectoryInfo in modulesDirectoryInfo.GetDirectories())
                {
                    LoadModules(moduleBuilders, moduleDirectoryInfo, autoRemoveAssemblies);
                }
            }
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="moduleBuilders"></param>
        /// <param name="directoryInfo"></param>
        /// <param name="autoRemoveAssemblies"></param>
        /// <returns></returns>
        private static void LoadModules(IEnumerable<IModuleBuilder> moduleBuilders, DirectoryInfo directoryInfo, bool autoRemoveAssemblies)
        {
            ModuleDirectoryInfo moduleDirectoryInfo = new(directoryInfo, moduleBuilders, autoRemoveAssemblies);
            MergeBlockHost.ModuleDirectoryInfos.Add(moduleDirectoryInfo);
        }
        /// <summary>
        /// 运行模块
        /// </summary>
        /// <param name="func"></param>
        /// <param name="enableDepend">启用依赖</param>
        /// <returns></returns>
        private static async Task RunModuleAsync(Action<IModuleInfo> func, bool enableDepend = true) => await RunModuleAsync(async m =>
        {
            func.Invoke(m);
            await Task.CompletedTask;
        }, enableDepend);
        /// <summary>
        /// 运行模块
        /// </summary>
        /// <param name="func"></param>
        /// <param name="enableDepend">启用依赖</param>
        /// <returns></returns>
        private static async Task RunModuleAsync(Func<IModuleInfo, Task> func, bool enableDepend = true)
        {
            Queue<IModuleInfo> waitingModules = new(MergeBlockHost.ModuleInfos);
            List<string> complateModuleNames = [];
            while (waitingModules.Count > 0)
            {
                IModuleInfo moduleInfo = waitingModules.Dequeue();
                if (enableDepend)
                {
                    if (complateModuleNames.Intersect(moduleInfo.Depends).Count() == moduleInfo.Depends.Length)
                    {
                        await func.Invoke(moduleInfo);
                        complateModuleNames.Add(moduleInfo.Name);
                    }
                    else
                    {
                        waitingModules.Enqueue(moduleInfo);
                    }
                }
                else
                {
                    await func.Invoke(moduleInfo);
                    complateModuleNames.Add(moduleInfo.Name);
                }
            }
        }
        #endregion
    }
}
