using Materal.Abstractions;
using Materal.TFMS.EventBus;
using Materal.Utils.Http;
using RC.EnvironmentServer.IntegrationEventHandlers;
using RC.EnvironmentServer.Services;
using XMJ.Authority.IntegrationEvents;

namespace RC.EnvironmentServer.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public partial class Program
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="args"></param>
        /// <param name="services"></param>
        /// <param name="app"></param>
        /// <returns></returns>
        public override async Task InitAsync(string[] args, IServiceProvider services, WebApplication app)
        {
            #region ��ʼ������
            IConfigurationItemService configurationItemService = services.GetRequiredService<IConfigurationItemService>();
            await configurationItemService.InitAsync();
            #endregion
            #region �����¼�
            IEventBus eventBus = services.GetRequiredService<IEventBus>();
            await eventBus.SubscribeAsync<ProjectDeleteEvent, ProjectDeleteEventHandler>();
            await eventBus.SubscribeAsync<NamespaceDeleteEvent, NamespaceDeleteEventHandler>();
            await eventBus.SubscribeAsync<SyncConfigEvent, SyncConfigEventHandler>();
            eventBus.StartListening();
            #endregion
            await base.InitAsync(args, services, app);
        }
    }
}