using ConfigCenter.Environment.Services;
using ConfigCenter.IntegrationEvents;
using Materal.TFMS.EventBus;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.IntegrationEventHandlers
{
    public class EditProjectEventHandler : IIntegrationEventHandler<EditProjectEvent>
    {
        private readonly IConfigurationItemService _configurationItemService;

        public EditProjectEventHandler(IConfigurationItemService configurationItemService)
        {
            _configurationItemService = configurationItemService;
        }

        public async Task HandleAsync(EditProjectEvent @event)
        {
            await _configurationItemService.EditAllProjectName(@event.OldProjectName, @event.NewProjectName);
        }
    }
}
