using AutoMapper;
using ConfigCenter.Environment.Services;
using ConfigCenter.Environment.Services.Models.ConfigurationItem;
using ConfigCenter.IntegrationEvents;
using Materal.TFMS.EventBus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigCenter.Environment.Common;

namespace ConfigCenter.Environment.IntegrationEventHandlers
{
    public class SyncConfigurationItemEventHandler : IIntegrationEventHandler<SyncConfigurationItemEvent>
    {
        private readonly IConfigurationItemService _configurationItemService;
        private readonly IMapper _mapper;

        public SyncConfigurationItemEventHandler(IConfigurationItemService configurationItemService, IMapper mapper)
        {
            _configurationItemService = configurationItemService;
            _mapper = mapper;
        }

        public async Task HandleAsync(SyncConfigurationItemEvent @event)
        {
            if (!@event.TargetsAPI.Contains(ConfigCenterEnvironmentConfig.ServiceName)) return;
            List<AddConfigurationItemModel> model = _mapper.Map<List<AddConfigurationItemModel>>(@event.ConfigurationItem);
            await _configurationItemService.InitConfigurationItemsAsync(model);
        }
    }
}
