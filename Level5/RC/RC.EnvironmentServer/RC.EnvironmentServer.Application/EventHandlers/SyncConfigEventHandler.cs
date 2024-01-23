using Materal.MergeBlock.Abstractions.Config;
using Materal.MergeBlock.Repository;
using Materal.TFMS.EventBus;
using Materal.Utils.Consul.ConfigModels;
using Microsoft.Extensions.Options;
using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.Enums;
using RC.EnvironmentServer.Abstractions.Events;

namespace RC.EnvironmentServer.Application.EventHandlers
{
    /// <summary>
    /// 同步配置事件处理器
    /// </summary>
    public class SyncConfigEventHandler(IOptionsMonitor<ConsulConfigModel> config, IMapper mapper, IMergeBlockUnitOfWork unitOfWork, IConfigurationItemRepository configurationItemRepository) : IIntegrationEventHandler<SyncConfigEvent>
    {
        private static readonly object _syncLockObj = new();
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandleAsync(SyncConfigEvent @event)
        {
            if (@event.TargetEnvironments.Length == 0 || !@event.TargetEnvironments.Contains(config.CurrentValue.ServiceName)) return;
            lock (_syncLockObj)
            {
                switch (@event.Mode)
                {
                    case SyncModeEnum.Mission:
                        AddItems(@event.ConfigurationItems);
                        break;
                    case SyncModeEnum.Replace:
                        ReplaceItems(@event.ConfigurationItems);
                        break;
                    case SyncModeEnum.Cover:
                        List<Guid> projectIDs = @event.ConfigurationItems.Select(m => m.ProjectID).Distinct().ToList();
                        if (projectIDs.Count > 1) break;
                        ClearItemsByProjectID(projectIDs.First());
                        AddItems(@event.ConfigurationItems);
                        break;
                }
            }
            await Task.CompletedTask;
        }
        /// <summary>
        /// 添加项
        /// </summary>
        /// <param name="configurationItems"></param>
        /// <returns></returns>
        private void AddItems(List<ConfigurationItemListDTO> configurationItems)
        {
            List<ConfigurationItem> allConfigurationItems = configurationItemRepository.Find(m => true);
            foreach (ConfigurationItemListDTO item in configurationItems)
            {
                if (allConfigurationItems.Any(m => m.ProjectID == item.ProjectID && m.NamespaceID == item.NamespaceID && m.Key == item.Key)) continue;
                ConfigurationItem target = mapper.Map<ConfigurationItem>(item);
                target.ID = Guid.NewGuid();
                unitOfWork.RegisterAdd(target);
            }
            unitOfWork.Commit();
        }
        /// <summary>
        /// 替换项
        /// </summary>
        /// <param name="configurationItems"></param>
        /// <returns></returns>
        private void ReplaceItems(List<ConfigurationItemListDTO> configurationItems)
        {
            List<ConfigurationItem> allConfigurationItems = configurationItemRepository.Find(m => true);
            foreach (ConfigurationItemListDTO item in configurationItems)
            {
                ConfigurationItem? target = allConfigurationItems.FirstOrDefault(m => m.ProjectID == item.ProjectID && m.NamespaceID == item.NamespaceID && m.Key == item.Key);
                if (target == null) continue;
                mapper.Map(item, target);
                unitOfWork.RegisterEdit(target);
            }
            unitOfWork.Commit();
        }
        /// <summary>
        /// 根据项目唯一标识清空项
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        private void ClearItemsByProjectID(Guid projectID)
        {
            List<ConfigurationItem> allConfigurationItems = configurationItemRepository.Find(m => m.ProjectID == projectID);
            foreach (ConfigurationItem item in allConfigurationItems)
            {
                unitOfWork.RegisterDelete(item);
            }
            unitOfWork.Commit();
        }
    }
}
