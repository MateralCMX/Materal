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
    /// ������
    /// </summary>
    public class Program : RCProgram
    {
        /// <summary>
        /// ��ں���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            WebApplication app = RCStart(args, services =>
            {
                services.AddEnvironmentServerService();
            }, null, "RC.EnvironmentServer");
            #region Ǩ�����ݿ�
            MigrateHelper<EnvironmentServerDBContext> migrateHelper = MateralServices.GetService<MigrateHelper<EnvironmentServerDBContext>>();
            await migrateHelper.MigrateAsync();
            #endregion
            #region ��ʼ������
            IConfigurationItemService configurationItemService = MateralServices.GetService<IConfigurationItemService>();
            await configurationItemService.InitAsync();
            #endregion
            #region �����¼�
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