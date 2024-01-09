namespace MMB.Demo.Application
{
    /// <summary>
    /// Demo模块
    /// </summary>
    public class DemoModule : MMBModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DemoModule() : base("Demo") { }
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
            IUserService userService = context.ServiceProvider.GetRequiredService<IUserService>();
            await userService.AddDefaultUserAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}
