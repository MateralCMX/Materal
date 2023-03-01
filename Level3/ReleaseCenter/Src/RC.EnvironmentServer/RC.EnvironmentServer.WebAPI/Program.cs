using Materal.Abstractions;
using Materal.TFMS.EventBus;
using Materal.Utils.Http;
using RC.EnvironmentServer.IntegrationEventHandlers;
using RC.EnvironmentServer.Services;
using XMJ.Authority.IntegrationEvents;

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
            IConfigurationItemService configurationItemService = MateralServices.GetService<IConfigurationItemService>();
            await configurationItemService.InitAsync();
            #endregion
            #region 订阅事件
            IEventBus eventBus = MateralServices.GetService<IEventBus>();
            await eventBus.SubscribeAsync<ProjectDeleteEvent, ProjectDeleteEventHandler>();
            await eventBus.SubscribeAsync<NamespaceDeleteEvent, NamespaceDeleteEventHandler>();
            await eventBus.SubscribeAsync<SyncConfigEvent, SyncConfigEventHandler>();
            eventBus.StartListening();
            #endregion
            await base.InitAsync(args, services, app);
        }
    }
}