using Microsoft.Extensions.DependencyInjection.Extensions;
using MMB.Demo.Application.Services;

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
        public DemoModule() : base("MMBDemo模块", "MMB.Demo")
        {
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task OnConfigServiceAsync(IConfigServiceContext context)
        {
            context.Services.Configure<ApplicationConfig>(context.Configuration);
            context.Services.TryAddScoped<IUserService, UserServiceImpl>();
            await base.OnConfigServiceAsync(context);
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
