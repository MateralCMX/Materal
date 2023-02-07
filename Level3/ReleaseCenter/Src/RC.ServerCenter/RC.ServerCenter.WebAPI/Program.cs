using Materal.Common;
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
            IEventBus? eventBus = MateralServices.GetServiceOrDefatult<IEventBus>();
            eventBus?.StartListening();
            await base.InitAsync(args, services, app);
        }
    }
}