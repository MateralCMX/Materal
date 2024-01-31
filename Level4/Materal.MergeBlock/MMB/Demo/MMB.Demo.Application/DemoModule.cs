namespace MMB.Demo.Application
{
    /// <summary>
    /// Demo模块
    /// </summary>
    public class DemoModule : MMBModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DemoModule() : base("MMBDemo模块", "MMB.Demo", ["MMB.Demo.Repository"])
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
            context.Services.Configure<ApplicationConfig>(context.Configuration.GetSection("MMB.Demo"));
            context.Services.AddConsulConfig("MMBDemo", ["MMB.Demo"]);
        }
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnApplicationInitAsync(IApplicationContext context)
        {
            using IServiceScope scope = context.ServiceProvider.CreateScope();
            IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            await userService.AddDefaultUserAsync();
            await base.OnApplicationInitAsync(context);
        }
    }
}
