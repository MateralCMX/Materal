using Microsoft.Extensions.Configuration;
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
        /// 构造函数
        /// </summary>
        public EnvironmentServerModule() : base("RCEnvironmentServer模块", "RC.EnvironmentServer", ["RC.EnvironmentServer.Repository", "Authorization"])
        {
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            await base.OnConfigServiceAsync(context);
            IConfigurationSection configurationSection = context.Configuration.GetSection("RC.EnvironmentServer");
            context.Services.Configure<ApplicationConfig>(configurationSection);
            string? serviceName = configurationSection.GetValue("ServiceName") ?? "RCESAPI";
            string? serviceDescription = configurationSection.GetValue("ServiceDescription") ?? "RC环境服务";
            context.Services.AddConsulConfig(serviceName, ["RC.EnvironmentServer", serviceDescription]);
            context.Services.TryAddSingleton<IServerController, ServerControllerAccessor>();
            context.Services.TryAddSingleton<IProjectController, ProjectControllerAccessor>();
            context.Services.TryAddSingleton<INamespaceController, NamespaceControllerAccessor>();
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
            await configurationItemService.InitAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}