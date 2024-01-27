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
        /// <returns></returns>
        public abstract Task RunAsync(string[] args);
        /// <summary>
        /// 配置模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public virtual async Task ConfigModuleAsync(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddOptions();
            services.Configure<MergeBlockConfig>(configuration);
            services.AddMateralUtils();
            TConfigServiceContext context = GetConfigServiceContext();
            LoadModules(context.ServiceProvider);
            List<IModuleInfo> allMergeBlockModules = MergeBlockHost.ModuleInfos;
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
            await ConfigServiceBeforeAsync(context);
            await RunModuleAsync(async m => await m.ConfigServiceBeforeAsync(context));
            await ConfigServiceAsync(context);
            await RunModuleAsync(async m => await m.ConfigServiceAsync(context));
            await ConfigServiceAfterAsync(context);
            await RunModuleAsync(async m => await m.ConfigServiceAfterAsync(context));
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
        public virtual async Task InitModuleAsync()
        {
            TApplicationContext context = GetApplicationContext();
            await ApplicationInitBeforeAsync(context);
            await RunModuleAsync(async m => await m.ApplicationInitBeforeAsync(context));
            await ApplicationInitAsync(context);
            await RunModuleAsync(async m => await m.ApplicationInitAsync(context));
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
        /// <returns></returns>
        private static void LoadModules(IServiceProvider serviceProvider)
        {
            IEnumerable<IModuleBuilder> moduleBuilders = serviceProvider.GetServices<IModuleBuilder>();
            IOptionsMonitor<MergeBlockConfig> mergeBlockConfig = serviceProvider.GetRequiredService<IOptionsMonitor<MergeBlockConfig>>();
            string rootModulesPath = AppDomain.CurrentDomain.BaseDirectory;
            LoadModules(moduleBuilders, new DirectoryInfo(rootModulesPath));
            string defaultModulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules");
            string[] modulesDirectoryPaths = [defaultModulesPath, .. mergeBlockConfig.CurrentValue.ModulesDirectories];
            foreach (string moduleDirectoryPath in modulesDirectoryPaths)
            {
                DirectoryInfo modulesDirectoryInfo = new(moduleDirectoryPath);
                if (!modulesDirectoryInfo.Exists) continue;
                foreach (DirectoryInfo moduleDirectoryInfo in modulesDirectoryInfo.GetDirectories())
                {
                    LoadModules(moduleBuilders, moduleDirectoryInfo);
                }
            }
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="moduleBuilders"></param>
        /// <param name="directoryInfo"></param>
        /// <returns></returns>
        private static void LoadModules(IEnumerable<IModuleBuilder> moduleBuilders, DirectoryInfo directoryInfo)
        {
            ModuleDirectoryInfo moduleDirectoryInfo = new(directoryInfo, moduleBuilders);
            MergeBlockHost.ModuleDirectoryInfos.Add(moduleDirectoryInfo);
        }
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
