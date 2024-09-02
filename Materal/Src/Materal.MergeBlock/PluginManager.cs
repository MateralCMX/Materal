namespace Materal.MergeBlock
{
    /// <summary>
    /// 插件管理器
    /// </summary>
    internal class PluginManager
    {
        /// <summary>
        /// 插件列表
        /// </summary>
        public IEnumerable<IPlugin> Plugins { get; set; }
        public PluginManager()
        {
            PluginAccessor pluginAccessor = new();
            pluginAccessor.LoadPlugins();
            PluginLoadContext.AssemblyLoadHandler += PluginLoadContext_AssemblyLoadHandler;
            Plugins = pluginAccessor.Plugins;
            SetPluginOrder();
        }
        /// <summary>
        /// 设置插件顺序
        /// </summary>
        private void SetPluginOrder()
        {
            List<IPlugin> deps = [];
            foreach (IPlugin plugin in Plugins)
            {
                SetPluginsDependencyList(plugin, deps);
                if (deps.Contains(plugin)) continue;
                deps.Add(plugin);
            }
            Plugins = deps;
        }
        /// <summary>
        /// 设置插件依赖列表
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="deps"></param>
        /// <exception cref="MergeBlockException"></exception>
        private void SetPluginsDependencyList(IPlugin plugin, IList<IPlugin> deps)
        {
            if (plugin.DependentPlugins == null || plugin.DependentPlugins.Count <= 0) return;
            foreach (string dependentPluginName in plugin.DependentPlugins)
            {
                IPlugin dependentPlugin = Plugins.FirstOrDefault(p => p.Name == dependentPluginName) ?? throw new MergeBlockException($"插件[{plugin.Name}]所依赖的插件[{dependentPluginName}]不存在！");
                if (dependentPlugin.DependentPlugins is not null)
                {
                    SetPluginsDependencyList(dependentPlugin, deps);
                }
                if (deps.Contains(dependentPlugin)) continue;
                deps.Add(dependentPlugin);
            }
        }
        /// <summary>
        /// 插件加载上下文程序集加载处理
        /// </summary>
        /// <param name="pluginName"></param>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        private Assembly? PluginLoadContext_AssemblyLoadHandler(string pluginName, AssemblyName assemblyName)
        {
            if (!Plugins.Any()) return null;
            IPlugin? plugin = Plugins.FirstOrDefault(m => m.Name == pluginName);
            if (plugin is null || plugin.DependentPlugins is null) return null;
            foreach (string dependentPlugin in plugin.DependentPlugins)
            {
                string name = dependentPlugin;
                Assembly? assembly = Plugins.First(m => m.Name == name).LoadContext?.Assemblies.FirstOrDefault(_ass => _ass.GetName().Name == assemblyName.Name);
                if (assembly is not null) return assembly;
            }
            return null;
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        public void LoadModules()
        {
            foreach (IPlugin plugin in Plugins)
            {
                MateralServices.Logger?.LogDebug($"加载插件[{plugin.Name}]");
                ExposeServices(plugin);
                ConfigOptions(plugin);
                LoadModules(plugin);
            }
            SetModulesOrder();
        }
        /// <summary>
        /// 导出服务
        /// </summary>
        /// <param name="plugin"></param>
        private static void ExposeServices(IPlugin plugin)
        {
            if (!plugin.PluginType.HasFlag(PluginType.Service)) return;
            foreach (Assembly assembly in plugin.Assemblies)
            {
                MateralServices.Services.AddAutoService(assembly);
            }
        }
        /// <summary>
        /// 配置选项
        /// </summary>
        /// <param name="plugin"></param>
        private static void ConfigOptions(IPlugin plugin)
        {
            if (MateralServices.Configuration is null) return;
            MethodInfo? method = typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod("Configure", BindingFlags.Static | BindingFlags.Public, [typeof(IServiceCollection), typeof(IConfiguration)]);
            if (method is null) return;
            foreach (Assembly assembly in plugin.Assemblies)
            {
                foreach (Type element in assembly.GetTypesByFilter(m => m.IsPublic && m.IsClass && !m.IsAbstract && !m.IsGenericType && typeof(IOptions).IsAssignableFrom(m)))
                {
                    MethodInfo methodInfo = method.MakeGenericMethod(element);
                    string key;
                    OptionsAttribute? optionsAttribute = element.GetCustomAttribute<OptionsAttribute>();
                    if (optionsAttribute is null)
                    {
                        key = element.Name;
                        if (key.EndsWith("Options"))
                        {
                            key = key[..^7];
                        }
                    }
                    else
                    {
                        key = optionsAttribute.SectionName;
                    }
                    IConfigurationSection section = MateralServices.Configuration.GetSection(key);
                    object?[] parameters = [MateralServices.Services, section];
                    methodInfo.Invoke(null, parameters);
                }
            }
        }
        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="plugin"></param>
        private static void LoadModules(IPlugin plugin)
        {
            if (!plugin.PluginType.HasFlag(PluginType.Module)) return;
            if (plugin.StartModuleType != null)
            {
                ModuleDescriptor moduleDescriptor = ModuleLoader.LoadModules(plugin.StartModuleType, plugin.Name).First(m => m.Type == plugin.StartModuleType);
                plugin.Modules[plugin.StartModuleType] = moduleDescriptor;
            }
            else
            {
                foreach (Assembly assembly in plugin.Assemblies)
                {
                    foreach (Type type in assembly.GetTypesByFilter(IMergeBlockModule.IsMergeBlockModule))
                    {
                        ModuleDescriptor moduleDescriptor = ModuleLoader.LoadModules(type, plugin.Name).First(m => m.Type == type);
                        plugin.Modules[type] = moduleDescriptor;
                    }
                }
            }
        }
        /// <summary>
        /// 设置模块顺序
        /// </summary>
        /// <param name="module"></param>
        /// <param name="modules"></param>
        private static void SetModulesOrder(ModuleDescriptor? module = null, List<ModuleDescriptor>? modules = null)
        {
            modules ??= [];
            if (module == null)
            {
                foreach (ModuleDescriptor moduleDescriptor in ModuleLoader.ModuleDescriptors)
                {
                    SetModulesOrder(moduleDescriptor, modules);
                }
                ModuleLoader.ModuleDescriptors = modules;
            }
            else
            {
                if (module.Dependencies.Any())
                {
                    foreach (ModuleDescriptor dependency in (IEnumerable<ModuleDescriptor>)module.Dependencies)
                    {
                        SetModulesOrder(dependency, modules);
                    }
                }
                if (modules.Contains(module)) return;
                modules.Add(module);
            }
        }
    }
}
