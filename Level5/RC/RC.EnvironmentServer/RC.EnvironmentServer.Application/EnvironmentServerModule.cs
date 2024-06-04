using Microsoft.Extensions.Configuration;
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
            context.Services.AddHostedService<InitEnvironmentService>();
        }
    }
}