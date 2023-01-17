using Materal.BaseCore.EFRepository;
using Materal.TFMS.EventBus;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.Domain.Repositories;
using XMJ.Authority.IntegrationEvents;

namespace RC.EnvironmentServer.IntegrationEventHandlers
{
    public class NamespaceDeleteEventHandler : IIntegrationEventHandler<NamespaceDeleteEvent>
    {
        private readonly IConfigurationItemRepository _configurationItemRepository;
        private readonly IMateralCoreUnitOfWork _unitOfWork;
        public NamespaceDeleteEventHandler(IMateralCoreUnitOfWork unitOfWork, IConfigurationItemRepository configurationItemRepository)
        {
            _unitOfWork = unitOfWork;
            _configurationItemRepository = configurationItemRepository;
        }
        public async Task HandleAsync(NamespaceDeleteEvent @event)
        {
            List<ConfigurationItem> configurationItems = await _configurationItemRepository.FindAsync(m => m.NamespaceID == @event.NamespaceID);
            if (configurationItems.Count <= 0) return;
            foreach (ConfigurationItem item in configurationItems)
            {
                _unitOfWork.RegisterDelete(item);
            }
            await _unitOfWork.CommitAsync();
        }
    }
}
