namespace RC.Authority.Application
{
    /// <summary>
    /// Authority模块
    /// </summary>
    public class AuthorityModule : RCModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthorityModule() : base("RCAuthority模块", "RC.Authority", ["RC.Authority.Repository", "Authorization"])
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
            context.Services.Configure<ApplicationConfig>(context.Configuration.GetSection("Authority"));
            context.Services.AddConsulConfig("RCAuthority", ["RC.Authority"]);
        }
    }
}