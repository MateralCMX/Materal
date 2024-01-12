using Microsoft.Extensions.DependencyInjection.Extensions;
using RC.ServerCenter.Abstractions.HttpClient;

namespace RC.ServerCenter.Abstractions
{
    /// <summary>
    /// ServerCenter模块
    /// </summary>
    public class ServerCenterModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.TryAddScoped<IProjectController, ProjectControllerAccessor>();
            context.Services.TryAddScoped<INamespaceController, NamespaceControllerAccessor>();
            await base.OnConfigServiceAsync(context);
        }
    }
}
