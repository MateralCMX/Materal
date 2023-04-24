using AutoMapper;
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
        private static readonly object _syncLockObj = new();
        public SyncConfigEventHandler(IMapper mapper, IMateralCoreUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configurationItemRepository = unitOfWork.GetRepository<IConfigurationItemRepository>();
        }
        public Task HandleAsync(SyncConfigEvent @event)
        {
            if (@event.TargetEnvironments.Length == 0 || !@event.TargetEnvironments.Contains(WebAPIConfig.AppName)) return Task.CompletedTask;
            lock (_syncLockObj)
            {
                switch (@event.Mode)
                {
                    case Enums.SyncModeEnum.Mission:
                        AddItems(@event.ConfigurationItems);
                        break;
                    case Enums.SyncModeEnum.Replace:
                        ReplaceItems(@event.ConfigurationItems);
                        break;
                    case Enums.SyncModeEnum.Cover:
                        ClearItems();
                        AddItems(@event.ConfigurationItems);
                        break;
                }
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="configurationItems"></param>
        /// <returns></returns>
        private void AddItems(List<ConfigurationItemListDTO> configurationItems)
        {
            List<ConfigurationItem> allConfigurationItems = _configurationItemRepository.Find(m => true);
            foreach (ConfigurationItemListDTO item in configurationItems)
            {
                if (allConfigurationItems.Any(m => m.ProjectID == item.ProjectID && m.NamespaceID == item.NamespaceID && m.Key == item.Key)) continue;
                ConfigurationItem target = _mapper.Map<ConfigurationItem>(item);
                target.ID = Guid.NewGuid();
                _unitOfWork.RegisterAdd(target);
            }
            _unitOfWork.Commit();
        }
        /// <summary>
        /// 替换项
        /// </summary>
        /// <param name="configurationItems"></param>
        /// <returns></returns>
        private void ReplaceItems(List<ConfigurationItemListDTO> configurationItems)
        {
            List<ConfigurationItem> allConfigurationItems = _configurationItemRepository.Find(m => true);
            foreach (ConfigurationItemListDTO item in configurationItems)
            {
                ConfigurationItem? target = allConfigurationItems.FirstOrDefault(m => m.ProjectID == item.ProjectID && m.NamespaceID == item.NamespaceID && m.Key == item.Key);
                if (target == null) continue;
                _mapper.Map(item, target);
                _unitOfWork.RegisterEdit(target);
            }
            _unitOfWork.Commit();
        }
        /// <summary>
        /// 清空项
        /// </summary>
        /// <returns></returns>
        private void ClearItems()
        {
            List<ConfigurationItem> allConfigurationItems = _configurationItemRepository.Find(m => true);
            foreach (ConfigurationItem item in allConfigurationItems)
            {
                _unitOfWork.RegisterDelete(item);
            }
            _unitOfWork.Commit();
        }
    }
}
