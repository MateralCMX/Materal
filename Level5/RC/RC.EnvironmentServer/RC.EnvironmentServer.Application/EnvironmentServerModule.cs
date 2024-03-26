using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RC.ServerCenter.Abstractions.ControllerAccessors;

namespace RC.EnvironmentServer.Application
{
    /// <summary>
    /// EnvironmentServer模块
    /// </summary>
    public class EnvironmentServerModule() : RCModule("RCEnvironmentServer模块", "RC.EnvironmentServer", ["RC.EnvironmentServer.Repository", "Authorization"])
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            await base.OnConfigServiceAsync(context);
            IConfigurationSection configurationSection = context.Configuration.GetSection("EnvironmentServer");
            context.Services.Configure<ApplicationConfig>(configurationSection);
            string? serviceName = configurationSection.GetConfigItemToString("ServiceName") ?? "RCES";
            string? serviceDescription = configurationSection.GetConfigItemToString("ServiceDescription") ?? "RC环境服务";
            context.Services.AddConsulConfig(serviceName, ["RC.EnvironmentServer", serviceDescription]);
            context.Services.AddServerCenterControllerAccessors();
        }
        /// <summary>
        /// 应用程序启动完毕
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override async Task OnApplicationStartdAsync(IServiceProvider serviceProvider)
        {
            ThreadPool.QueueUserWorkItem(async _ =>
            {
                using IServiceScope scope = serviceProvider.CreateScope();
                ILogger? logger = scope.ServiceProvider.GetService<ILogger<EnvironmentServerModule>>();
                PolicyBuilder policyBuilder = Policy.Handle<Exception>();
                AsyncRetryPolicy retryPolicy = policyBuilder.WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(5), (ex, index, timeSpan) =>
                {
                    logger?.LogWarning(ex, $"[{index}]初始化失败,5秒后重试");
                });
                await retryPolicy.ExecuteAsync(async () =>
                {
                    IConfigurationItemService configurationItemService = scope.ServiceProvider.GetRequiredService<IConfigurationItemService>();
                    await configurationItemService.InitAsync();
                    logger?.LogInformation("初始化完毕");
                });
            });
            await base.OnApplicationStartdAsync(serviceProvider);
        }
    }
}