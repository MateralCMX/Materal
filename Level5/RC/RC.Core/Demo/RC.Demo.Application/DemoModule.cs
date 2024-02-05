namespace RC.Demo.Application
{
    /// <summary>
    /// Demo模块
    /// </summary>
    public class DemoModule : RCModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DemoModule() : base("RCDemo模块", "RC.Demo", ["RC.Demo.Repository", "Authorization"])
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
            context.Services.Configure<ApplicationConfig>(context.Configuration.GetSection("Demo"));
            context.Services.AddConsulConfig("RCDemo", ["RC.Demo"]);
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