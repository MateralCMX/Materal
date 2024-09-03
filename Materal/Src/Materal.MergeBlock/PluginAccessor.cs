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
            MergeBlockOptions config = new();
            if (MateralServices.Configuration is not null)
            {
                config = MateralServices.Configuration.GetSection(MergeBlockOptions.ConfigKey).Get<MergeBlockOptions>() ?? config;
            }
            IEnumerable<ILoadPluginsService> loadPluginsServices = MateralServices.Services.BuildMateralServiceProvider().GetServices<ILoadPluginsService>();
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
