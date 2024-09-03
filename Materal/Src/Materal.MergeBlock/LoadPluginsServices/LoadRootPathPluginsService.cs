using Microsoft.Extensions.Options;

namespace Materal.MergeBlock.LoadPluginsServices
{
    [Dependency(RegisterMode = ServiceRegisterMode.Add)]
    internal class LoadRootPathPluginsService : ILoadPluginsService, ISingletonDependency<ILoadPluginsService>
    {
        public IEnumerable<IPlugin> GetPlugins(IOptionsMonitor<MergeBlockOptions> config)
        {
            if (!config.CurrentValue.LoadFromRootPath) return [];
            MateralServices.Logger?.LogDebug("从根目录加载插件...");
            IPlugin defaultPlugin = new Plugin("Default", AppContext.BaseDirectory);
            return [defaultPlugin];
        }
    }
}
