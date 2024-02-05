using Microsoft.Extensions.Configuration;

namespace RC.Deploy.Application
{
    /// <summary>
    /// Deploy模块
    /// </summary>
    public class DeployModule : RCModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DeployModule() : base("RCDeploy模块", "RC.Deploy", ["RC.Deploy.Repository", "Authorization"])
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
            context.Services.AddSignalR();
            IConfigurationSection configurationSection = context.Configuration.GetSection("Deploy");
            context.Services.Configure<ApplicationConfig>(configurationSection);
            string? serviceName = configurationSection.GetValue("ServiceName") ?? "RCDeploy";
            string? serviceDescription = configurationSection.GetValue("ServiceDescription") ?? "RC发布服务";
            context.Services.AddConsulConfig(serviceName, ["RC.Deploy", serviceDescription]);
        }
    }
}