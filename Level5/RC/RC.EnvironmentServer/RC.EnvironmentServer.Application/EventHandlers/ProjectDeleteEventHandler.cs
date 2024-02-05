using Materal.EventBus.RabbitMQ;
using RC.ServerCenter.Abstractions.Events;

namespace RC.EnvironmentServer.Application.EventHandlers
{
    /// <summary>
    /// 项目删除事件处理器
    /// </summary>
    public class ProjectDeleteEventHandler(IOptionsMonitor<ApplicationConfig> applicationConfig, IOptionsMonitor<EventBusConfig> eventBusConfig, IEnvironmentServerUnitOfWork unitOfWork, IConfigurationItemRepository configurationItemRepository) : RCEnvironmentServerEventHandler<ProjectDeleteEvent>(applicationConfig, eventBusConfig)
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public override async Task HandleAsync(ProjectDeleteEvent @event)
        {
            List<ConfigurationItem> configurationItems = await configurationItemRepository.FindAsync(m => m.ProjectID == @event.ProjectID);
            if (configurationItems.Count <= 0) return;
            foreach (ConfigurationItem item in configurationItems)
            {
                unitOfWork.RegisterDelete(item);
            }
            await unitOfWork.CommitAsync();
        }
    }
}