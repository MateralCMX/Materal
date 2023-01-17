using Materal.Common;
using Materal.TFMS.EventBus;
using Materal.TTA.EFRepository;
using RC.Core.WebAPI;
using RC.EnvironmentServer.EFRepository;
using RC.EnvironmentServer.IntegrationEventHandlers;
using RC.EnvironmentServer.Services;
using XMJ.Authority.IntegrationEvents;

namespace RC.EnvironmentServer.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program : RCProgram
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplication app = RCStart(args, services =>
            {
                services.AddEnvironmentServerService();
            }, null, "RC.EnvironmentServer");
            #region 迁移数据库
            MigrateHelper<EnvironmentServerDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<EnvironmentServerDBContext>>();
            await migrateHelper.MigrateAsync();
            #endregion
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
            await app.RunAsync();
        }
    }
}