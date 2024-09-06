using Materal.MergeBlock.Consul.Abstractions;
using Microsoft.Extensions.Configuration;
using RC.EnvironmentServer.Repository;
using RC.ServerCenter.Abstractions.ControllerAccessors;

namespace RC.EnvironmentServer.Application
{
    /// <summary>
    /// EnvironmentServer模块
    /// </summary>
    [DependsOn(typeof(EnvironmentServerRepositoryModule))]
    public class EnvironmentServerModule() : RCModule("RCEnvironmentServer模块")
    {
        /// <inheritdoc/>
        public override void OnConfigureServices(ServiceConfigurationContext context)
        {
            base.OnConfigureServices(context);
            if (context.Configuration is null) return;
            IConfigurationSection configurationSection = context.Configuration.GetSection("EnvironmentServer");
            string? serviceName = configurationSection.GetConfigItemToString("ServiceName") ?? "RCES";
            string? serviceDescription = configurationSection.GetConfigItemToString("ServiceDescription") ?? "RC环境服务";
            context.Services.AddConsulConfig(serviceName, ["RC.EnvironmentServer", serviceDescription]);
            context.Services.AddServerCenterControllerAccessors();
            context.Services.AddHostedService<InitEnvironmentService>();
        }
    }
}