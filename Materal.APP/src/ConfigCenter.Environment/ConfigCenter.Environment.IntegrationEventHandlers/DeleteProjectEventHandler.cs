using ConfigCenter.Environment.Services;
using ConfigCenter.IntegrationEvents;
using Materal.TFMS.EventBus;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.IntegrationEventHandlers
{
    public class DeleteProjectEventHandler : IIntegrationEventHandler<DeleteProjectEvent>
    {
        private readonly IConfigurationItemService _configurationItemService;

        public DeleteProjectEventHandler(IConfigurationItemService configurationItemService)
        {
            _configurationItemService = configurationItemService;
        }

        public async Task HandleAsync(DeleteProjectEvent @event)
        {
            await _configurationItemService.DeleteConfigurationItemByProjectAsync(@event.ProjectName);
        }
    }
}
