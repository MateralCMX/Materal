using Materal.TFMS.EventBus;
using RC.EnvironmentServer.Services;

namespace RC.EnvironmentServer.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public override async Task InitAsync(string[] args, IServiceProvider services, WebApplication app)
        {
            #region 初始化数据
            IConfigurationItemService configurationItemService = services.GetRequiredService<IConfigurationItemService>();
            await configurationItemService.InitAsync();
            #endregion
            services.GetRequiredService<IEventBus>().StartListening();
            await base.InitAsync(args, services, app);
        }
    }
}