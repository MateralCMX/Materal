using Materal.MergeBlock.Repository;
using Materal.TFMS.EventBus;
using RC.ServerCenter.Abstractions.Events;

namespace RC.EnvironmentServer.Application.EventHandlers
{
    /// <summary>
    /// 项目删除事件处理器
    /// </summary>
    public class ProjectDeleteEventHandler(IMergeBlockUnitOfWork unitOfWork, IConfigurationItemRepository configurationItemRepository) : IIntegrationEventHandler<ProjectDeleteEvent>
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleAsync(ProjectDeleteEvent @event)
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