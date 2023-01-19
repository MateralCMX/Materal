using Materal.BaseCore.EFRepository;
using Materal.TFMS.EventBus;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.Domain.Repositories;
using XMJ.Authority.IntegrationEvents;

namespace RC.EnvironmentServer.IntegrationEventHandlers
{
    /// <summary>
    /// 用户删除事件处理
    /// </summary>
    public class ProjectDeleteEventHandler : IIntegrationEventHandler<ProjectDeleteEvent>
    {
        private readonly IConfigurationItemRepository _configurationItemRepository;
        private readonly IMateralCoreUnitOfWork _unitOfWork;
        public ProjectDeleteEventHandler(IMateralCoreUnitOfWork unitOfWork, IConfigurationItemRepository configurationItemRepository)
        {
            _unitOfWork = unitOfWork;
            _configurationItemRepository = configurationItemRepository;
        }
        public async Task HandleAsync(ProjectDeleteEvent @event)
        {
            List<ConfigurationItem> configurationItems = await _configurationItemRepository.FindAsync(m => m.ProjectID == @event.ProjectID);
            if (configurationItems.Count <= 0) return;
            foreach (ConfigurationItem item in configurationItems)
            {
                _unitOfWork.RegisterDelete(item);
            }
            await _unitOfWork.CommitAsync();
        }
    }
}