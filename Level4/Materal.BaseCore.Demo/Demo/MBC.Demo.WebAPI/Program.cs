using Materal.BaseCore.EventBus;
using Materal.BaseCore.WebAPI.Common;
using MBC.Demo.Services;

namespace MBC.Demo.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigService(IServiceCollection services)
        {
            base.ConfigService(services);
            services.AddEventBus($"{WebAPIConfig.AppName}Queue", true);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public override async Task InitAsync(string[] args, IServiceProvider services, WebApplication app)
        {
            #region 添加默认用户
            IUserService? userService = services.GetRequiredService<IUserService>();
            await userService.AddDefaultUserAsync();
            #endregion
            await base.InitAsync(args, services, app);
        }
        /// <summary>
        /// 配置构建器
        /// </summary>
        /// <param name="builder"></param>
        public override void ConfigBuilder(WebApplicationBuilder builder)
        {
            base.ConfigBuilder(builder);
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = 256 * 1024 * 1024;
            });
        }
    }
}