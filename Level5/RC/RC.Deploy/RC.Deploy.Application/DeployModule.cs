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
            context.Services.Configure<ApplicationConfig>(context.Configuration.GetSection("RC.Deploy"));
            context.Services.AddConsulConfig("RCDeploy", ["RC.Deploy"]);
        }
    }
}