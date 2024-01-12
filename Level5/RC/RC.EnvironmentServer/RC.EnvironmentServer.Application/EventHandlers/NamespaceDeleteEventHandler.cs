using Materal.MergeBlock.Repository;
using Materal.TFMS.EventBus;
using RC.ServerCenter.Abstractions.Events;

namespace RC.EnvironmentServer.Application.EventHandlers
{
    /// <summary>
    /// 命名空间删除事件处理器
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="configurationItemRepository"></param>
    public class NamespaceDeleteEventHandler(IMergeBlockUnitOfWork unitOfWork, IConfigurationItemRepository configurationItemRepository) : IIntegrationEventHandler<NamespaceDeleteEvent>
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleAsync(NamespaceDeleteEvent @event)
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
