using AutoMapper;
using Consul;
using Materal.BaseCore.EFRepository;
using Materal.BaseCore.WebAPI.Common;
using Materal.TFMS.EventBus;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.Domain.Repositories;
using XMJ.Authority.IntegrationEvents;

namespace RC.EnvironmentServer.IntegrationEventHandlers
{
    public class SyncConfigEventHandler : IIntegrationEventHandler<SyncConfigEvent>
    {
        private readonly IMapper _mapper;
        private readonly IMateralCoreUnitOfWork _unitOfWork;
        private readonly IConfigurationItemRepository _configurationItemRepository;
        public SyncConfigEventHandler(IMapper mapper, IMateralCoreUnitOfWork unitOfWork, IConfigurationItemRepository configurationItemRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configurationItemRepository = configurationItemRepository;
        }
        public async Task HandleAsync(SyncConfigEvent @event)
        {
            if (@event.TargetEnvironments.Length == 0 || !@event.TargetEnvironments.Contains(WebAPIConfig.AppName)) return;
            switch (@event.Mode)
            {
                case Enums.SyncModeEnum.Mission:
                    await AddItemsAsync(@event.ConfigurationItems);
                    break;
                case Enums.SyncModeEnum.Replace:
                    await ReplaceItemsAsync(@event.ConfigurationItems);
                    break;
                case Enums.SyncModeEnum.Cover:
                    await ReplaceItemsAsync(@event.ConfigurationItems);
                    await AddItemsAsync(@event.ConfigurationItems);
                    break;
            }
            await _unitOfWork.CommitAsync();
        }
        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="configurationItems"></param>
        /// <returns></returns>
        private async Task AddItemsAsync(List<ConfigurationItemListDTO> configurationItems)
        {
            List<ConfigurationItem> allConfigurationItems = await _configurationItemRepository.GetAllInfoFromCacheAsync();
            foreach (ConfigurationItemListDTO item in configurationItems)
            {
                if (allConfigurationItems.Any(m => m.ProjectID == item.ProjectID && m.NamespaceID == item.NamespaceID && m.Key == item.Key)) continue;
                ConfigurationItem target = _mapper.Map<ConfigurationItem>(item);
                target.ID = Guid.NewGuid();
                _unitOfWork.RegisterAdd(target);
            }
        }
        /// <summary>
        /// 替换项
        /// </summary>
        /// <param name="configurationItems"></param>
        /// <returns></returns>
        private async Task ReplaceItemsAsync(List<ConfigurationItemListDTO> configurationItems)
        {
            List<ConfigurationItem> allConfigurationItems = await _configurationItemRepository.GetAllInfoFromCacheAsync();
            foreach (ConfigurationItemListDTO item in configurationItems)
            {
                ConfigurationItem? target = allConfigurationItems.FirstOrDefault(m => m.ProjectID == item.ProjectID && m.NamespaceID == item.NamespaceID && m.Key == item.Key);
                if (target == null) continue;
                _mapper.Map(item, target);
                _unitOfWork.RegisterEdit(target);
            }
        }
    }
}
