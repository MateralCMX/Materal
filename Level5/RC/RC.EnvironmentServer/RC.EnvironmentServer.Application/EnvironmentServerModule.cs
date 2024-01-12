using Microsoft.Extensions.DependencyInjection.Extensions;
using RC.ServerCenter.Abstractions.Controllers;
using RC.ServerCenter.Abstractions.HttpClient;

namespace RC.EnvironmentServer.Application
{
    /// <summary>
    /// EnvironmentServer模块
    /// </summary>
    public class EnvironmentServerModule : RCModule, IMergeBlockModule
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            await base.OnConfigServiceAsync(context);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            using IServiceScope scope = context.ServiceProvider.CreateScope();
            IConfigurationItemService configurationItemService = scope.ServiceProvider.GetRequiredService<IConfigurationItemService>();
            configurationItemService.ServiceProvider = scope.ServiceProvider;
            await configurationItemService.InitAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}
