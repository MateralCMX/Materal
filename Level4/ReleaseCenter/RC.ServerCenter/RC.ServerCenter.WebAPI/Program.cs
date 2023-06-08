using Materal.TFMS.EventBus;

namespace RC.ServerCenter.WebAPI
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
            services.GetRequiredService<IEventBus>().StartListening();
            await base.InitAsync(args, services, app);
        }
    }
}