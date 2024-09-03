using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.LoadPluginsServices
{
    [Dependency(RegisterMode = ServiceRegisterMode.Add)]
    internal class LoadPluginsFolderPluginsService : ILoadPluginsService, ISingletonDependency<ILoadPluginsService>
    {
        public IEnumerable<IPlugin> GetPlugins(IOptionsMonitor<MergeBlockOptions> config)
        {
            if (!config.CurrentValue.LoadFromPlugins) return [];
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
    }
}
