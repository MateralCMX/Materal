using Materal.EventBus.RabbitMQ;
using RC.ServerCenter.Abstractions.Events;

namespace RC.EnvironmentServer.Application.EventHandlers
{
    /// <summary>
    /// 命名空间删除事件处理器
    /// </summary>
    public class NamespaceDeleteEventHandler(IOptionsMonitor<ApplicationConfig> applicationConfig, IOptionsMonitor<RabbitMQEventBusOptions> eventBusConfig, IEnvironmentServerUnitOfWork unitOfWork, IConfigurationItemRepository configurationItemRepository) : RCEnvironmentServerEventHandler<NamespaceDeleteEvent>(applicationConfig, eventBusConfig)
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public override async Task HandleAsync(NamespaceDeleteEvent @event)
        {
            List<ConfigurationItem> configurationItems = await configurationItemRepository.FindAsync(m => m.NamespaceID == @event.NamespaceID);
            if (configurationItems.Count <= 0) return;
            foreach (ConfigurationItem item in configurationItems)
            {
                unitOfWork.RegisterDelete(item);
            }
            await unitOfWork.CommitAsync();
        }
    }
}
