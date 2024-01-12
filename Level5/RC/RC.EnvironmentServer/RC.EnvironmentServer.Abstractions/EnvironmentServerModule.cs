using Microsoft.Extensions.DependencyInjection.Extensions;
using RC.EnvironmentServer.Abstractions.HttpClient;

namespace RC.EnvironmentServer.Application
{
    /// <summary>
    /// EnvironmentServer模块
    /// </summary>
    public class EnvironmentServerModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.TryAddScoped<IConfigurationItemController, ConfigurationItemControllerAccessor>();
            await base.OnConfigServiceAsync(context);
        }
    }
}
