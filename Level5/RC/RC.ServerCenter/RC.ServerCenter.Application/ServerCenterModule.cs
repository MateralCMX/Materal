namespace RC.ServerCenter.Application
{
    /// <summary>
    /// ServerCenter模块
    /// </summary>
    public class ServerCenterModule : RCModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServerCenterModule() : base("RCServerCenter模块", "RC.ServerCenter", ["RC.ServerCenter.Repository", "Authorization"])
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
            context.Services.Configure<ApplicationConfig>(context.Configuration.GetSection("ServerCenter"));
            context.Services.AddConsulConfig("RCServerCenter", ["RC.ServerCenter"]);
        }
    }
}