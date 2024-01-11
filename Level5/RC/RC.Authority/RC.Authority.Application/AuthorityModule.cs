namespace RC.Authority.Application
{
    /// <summary>
    /// Authority模块
    /// </summary>
    public class AuthorityModule : RCModule, IMergeBlockModule
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
    }
}
