using ConfigCenter.Environment.Services;
using ConfigCenter.IntegrationEvents;
using Materal.TFMS.EventBus;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.IntegrationEventHandlers
{
    public class DeleteNamespaceEventHandler : IIntegrationEventHandler<DeleteNamespaceEvent>
    {
        private readonly IConfigurationItemService _configurationItemService;

        public DeleteNamespaceEventHandler(IConfigurationItemService configurationItemService)
        {
            _configurationItemService = configurationItemService;
        }

        public async Task HandleAsync(DeleteNamespaceEvent @event)
        {
            await _configurationItemService.DeleteConfigurationItemByNamespaceAsync(@event.ProjectName, @event.NamespaceName);
        }
    }
}
