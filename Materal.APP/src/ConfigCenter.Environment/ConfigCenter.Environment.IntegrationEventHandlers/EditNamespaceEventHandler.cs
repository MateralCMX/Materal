using ConfigCenter.Environment.Services;
using ConfigCenter.IntegrationEvents;
using Materal.TFMS.EventBus;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.IntegrationEventHandlers
{
    public class EditNamespaceEventHandler : IIntegrationEventHandler<EditNamespaceEvent>
    {
        private readonly IConfigurationItemService _configurationItemService;

        public EditNamespaceEventHandler(IConfigurationItemService configurationItemService)
        {
            _configurationItemService = configurationItemService;
        }

        public async Task HandleAsync(EditNamespaceEvent @event)
        {
            await _configurationItemService.EditAllNamespaceName(@event.ProjectName, @event.OldNamespaceName, @event.NewNamespaceName);
        }
    }
}
