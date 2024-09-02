namespace Materal.MergeBlock
{
    /// <summary>
    /// 插件访问器
    /// </summary>
    public class PluginAccessor
    {
        /// <summary>
        /// 插件列表
        /// </summary>
        public List<IPlugin> Plugins { get; private set; } = [];
        /// <summary>
        /// 加载插件
        /// </summary>
        public void LoadPlugins()
        {
            Plugins.AddRange(LoadPluginsFromRootPath());
            Plugins.AddRange(LoadPluginsFromPlugins());
            Plugins.AddRange(LoadPluginsFormPluginsConfig());
            foreach (IPlugin plugin in Plugins)
            {
                plugin.EnsurePlugin();
            }
        }
        /// <summary>
        /// 从根路径加载插件
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<IPlugin> LoadPluginsFromRootPath()
        {
            IConfigurationSection? section = MateralServices.Configuration?.GetSection("MergeBlock:LoadFromRootPath");
            bool isLoad = true;
            if (section is not null && !string.IsNullOrWhiteSpace(section.Value))
            {
                isLoad = section.Get<bool>();
            }
            if (!isLoad) return [];
            MateralServices.Logger?.LogDebug("从根目录加载插件...");
            IPlugin defaultPlugin = new Plugin("Default", AppContext.BaseDirectory);
            return [defaultPlugin];
        }
        /// <summary>
        /// 从Plugins文件夹加载插件
        /// </summary>
        /// <returns></returns>
        private static List<IPlugin> LoadPluginsFromPlugins()
        {
            IConfigurationSection? section = MateralServices.Configuration?.GetSection("MergeBlock:LoadFromPlugins");
            bool isLoad = true;
            if (section is not null && !string.IsNullOrWhiteSpace(section.Value))
            {
                isLoad = section.Get<bool>();
            }
            if (!isLoad) return [];
            string pluginsPath = Path.Combine(AppContext.BaseDirectory, "Plugins");
            DirectoryInfo directoryInfo = new(pluginsPath);
            if (!directoryInfo.Exists) return [];
            MateralServices.Logger?.LogDebug("从插件目录[Plugins]加载插件...");
            List<IPlugin> plugins = [];
            foreach (DirectoryInfo pluginDirectoryInfo in directoryInfo.GetDirectories())
            {
                IPlugin plugin = new Plugin(pluginDirectoryInfo.Name, pluginDirectoryInfo.FullName);
                plugins.Add(plugin);
            }
            return plugins;
        }
        private static List<Plugin> LoadPluginsFormPluginsConfig()
        {
            IConfigurationSection? section = MateralServices.Configuration?.GetSection("MergeBlock:Plugins");
            if (section is null || string.IsNullOrWhiteSpace(section.Value)) return [];
            MateralServices.Logger?.LogDebug("从配置文件加载插件...");
            List<Plugin> plugins = section.Get<List<Plugin>>() ?? [];
            return plugins;
        }
    }
}
