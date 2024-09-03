using Materal.MergeBlock.LoadPluginsServices;
using Microsoft.Extensions.Options;

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
            IServiceProvider serviceProvider = MateralServices.Services.BuildMateralServiceProvider();
            IOptionsMonitor<MergeBlockOptions> config = serviceProvider.GetRequiredService<IOptionsMonitor<MergeBlockOptions>>();
            IEnumerable<ILoadPluginsService> loadPluginsServices = serviceProvider.GetServices<ILoadPluginsService>();
            foreach (ILoadPluginsService loadPluginsService in loadPluginsServices)
            {
                Plugins.AddRange(loadPluginsService.GetPlugins(config));
            }
            foreach (IPlugin plugin in Plugins)
            {
                plugin.EnsurePlugin();
            }
        }
    }
}
