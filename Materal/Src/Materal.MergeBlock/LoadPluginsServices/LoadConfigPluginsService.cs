using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.LoadPluginsServices
{
    [Dependency(RegisterMode = ServiceRegisterMode.Add)]
    internal class LoadConfigPluginsService : ILoadPluginsService, ISingletonDependency<ILoadPluginsService>
    {
        public IEnumerable<IPlugin> GetPlugins(IOptionsMonitor<MergeBlockOptions> config)
        {
            IConfigurationSection? section = MateralServices.Configuration?.GetSection("MergeBlock:Plugins");
            if (section is null) return [];
            MateralServices.Logger?.LogDebug("从配置文件加载插件...");
            List<Plugin> plugins = section.Get<List<Plugin>>() ?? [];
            return plugins;
        }
    }
}
