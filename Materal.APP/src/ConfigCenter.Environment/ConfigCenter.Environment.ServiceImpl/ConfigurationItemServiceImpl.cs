using AutoMapper;
using ConfigCenter.Environment.Common;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.Domain;
using ConfigCenter.Environment.Domain.Repositories;
using ConfigCenter.Environment.Services;
using ConfigCenter.Environment.Services.Models.ConfigurationItem;
using ConfigCenter.Environment.SqliteEFRepository;
using Materal.ConvertHelper;
using Materal.LinqHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ConfigCenter.Environment.ServiceImpl
{
    public class ConfigurationItemServiceImpl : IConfigurationItemService
    {
        private readonly IConfigurationItemRepository _configurationItemRepository;
        private readonly IConfigCenterEnvironmentSqliteEFUnitOfWork _configCenterEnvironmentUnitOfWork;
        private readonly IMapper _mapper;

        public ConfigurationItemServiceImpl(IConfigurationItemRepository configurationItemRepository, IConfigCenterEnvironmentSqliteEFUnitOfWork configCenterEnvironmentUnitOfWork, IMapper mapper)
        {
            _configurationItemRepository = configurationItemRepository;
            _configCenterEnvironmentUnitOfWork = configCenterEnvironmentUnitOfWork;
            _mapper = mapper;
        }
        public async Task InitConfigurationItemsAsync(ICollection<AddConfigurationItemModel> model)
        {
            List<ConfigurationItem> allConfigurationItems = await _configurationItemRepository.FindAsync(m => true);
            foreach (AddConfigurationItemModel item in model)
            {
                ConfigurationItem configurationItem =  allConfigurationItems.FirstOrDefault(m => m.ProjectName == item.ProjectName && m.NamespaceName == item.NamespaceName && m.Key == item.Key);
                if (configurationItem == null)
                {
                    configurationItem = item.CopyProperties<ConfigurationItem>();
                    _configCenterEnvironmentUnitOfWork.RegisterAdd(configurationItem);
                }
                else
                {
                    configurationItem.Value = item.Value;
                    configurationItem.Description = item.Description;
                    _configCenterEnvironmentUnitOfWork.RegisterEdit(configurationItem);
                }
            }
            await _configCenterEnvironmentUnitOfWork.CommitAsync();
            await _configurationItemRepository.ClearCacheAsync();
        }

        public async Task AddConfigurationItemAsync(AddConfigurationItemModel model)
        {
            if (await _configurationItemRepository.ExistedAsync(m => m.Key.Equals(model.Key) && m.NamespaceName == model.NamespaceName && m.ProjectName == model.ProjectName)) throw new ConfigCenterEnvironmentException("Key已存在");
            var configurationItem = model.CopyProperties<ConfigurationItem>();
            _configCenterEnvironmentUnitOfWork.RegisterAdd(configurationItem);
            await _configCenterEnvironmentUnitOfWork.CommitAsync();
            await _configurationItemRepository.ClearCacheAsync();
        }

        public async Task EditConfigurationItemAsync(EditConfigurationItemModel model)
        {
            if (await _configurationItemRepository.ExistedAsync(m => m.Key.Equals(model.Key) && m.NamespaceName == model.NamespaceName && m.ProjectName == model.ProjectName && m.ID != model.ID)) throw new ConfigCenterEnvironmentException("Key已存在");
            ConfigurationItem configurationItemFromDb = await _configurationItemRepository.FirstOrDefaultAsync(model.ID);
            if (configurationItemFromDb == null) throw new ConfigCenterEnvironmentException("配置项不存在");
            model.CopyProperties(configurationItemFromDb);
            configurationItemFromDb.UpdateTime = DateTime.Now;
            _configCenterEnvironmentUnitOfWork.RegisterEdit(configurationItemFromDb);
            await _configCenterEnvironmentUnitOfWork.CommitAsync();
            await _configurationItemRepository.ClearCacheAsync();
        }

        public async Task DeleteConfigurationItemAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            ConfigurationItem configurationItemFromDb = await _configurationItemRepository.FirstOrDefaultAsync(id);
            if (configurationItemFromDb == null) throw new ConfigCenterEnvironmentException("配置项不存在");
            _configCenterEnvironmentUnitOfWork.RegisterDelete(configurationItemFromDb);
            await _configCenterEnvironmentUnitOfWork.CommitAsync();
            await _configurationItemRepository.ClearCacheAsync();
        }

        public async Task DeleteConfigurationItemByProjectAsync(string projectName)
        {
            List<ConfigurationItem> configurationItems = await _configurationItemRepository.FindAsync(m => m.ProjectName == projectName);
            foreach (ConfigurationItem configurationItem in configurationItems)
            {
                _configCenterEnvironmentUnitOfWork.RegisterDelete(configurationItem);
            }
            await _configCenterEnvironmentUnitOfWork.CommitAsync();
            await _configurationItemRepository.ClearCacheAsync();
        }

        public async Task DeleteConfigurationItemByNamespaceAsync(string projectName, string namespaceName)
        {
            List<ConfigurationItem> configurationItems = await _configurationItemRepository.FindAsync(m => m.NamespaceName == namespaceName);
            foreach (ConfigurationItem configurationItem in configurationItems)
            {
                _configCenterEnvironmentUnitOfWork.RegisterDelete(configurationItem);
            }
            await _configCenterEnvironmentUnitOfWork.CommitAsync();
            await _configurationItemRepository.ClearCacheAsync();
        }

        public async Task<ConfigurationItemDTO> GetConfigurationItemInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            ConfigurationItem configurationItemFromDb = await _configurationItemRepository.FirstOrDefaultAsync(id);
            if (configurationItemFromDb == null) throw new ConfigCenterEnvironmentException("配置项不存在");
            var result = _mapper.Map<ConfigurationItemDTO>(configurationItemFromDb);
            return result;
        }

        public async Task<List<ConfigurationItemListDTO>> GetConfigurationItemListAsync(QueryConfigurationItemFilterModel filterModel)
        {
            Expression<Func<ConfigurationItem, bool>> searchExpression = filterModel.GetSearchExpression<ConfigurationItem>();
            if (filterModel.NamespaceNames != null && filterModel.NamespaceNames.Length > 0)
            {
                Expression<Func<ConfigurationItem, bool>> temp = null;
                foreach (string namespaceName in filterModel.NamespaceNames)
                {
                    temp = temp == null ? m => m.NamespaceName.Equals(namespaceName) : temp.Or(m => m.NamespaceName.Equals(namespaceName));
                }
                searchExpression = searchExpression.And(temp);
            }
            List<ConfigurationItem> allConfigurationItems = await _configurationItemRepository.GetAllInfoFromCacheAsync();
            if (allConfigurationItems == null) return null;
            List<ConfigurationItem> configurationItemsFromDb = allConfigurationItems.Where(searchExpression.Compile()).OrderBy(m=>m.Key).ToList();
            var result = _mapper.Map<List<ConfigurationItemListDTO>>(configurationItemsFromDb);
            return result;
        }

        public async Task EditAllProjectName(string oldName, string newName)
        {
            List<ConfigurationItem> configurationItems = await _configurationItemRepository.FindAsync(m => m.ProjectName == oldName);
            foreach (ConfigurationItem configurationItem in configurationItems)
            {
                configurationItem.ProjectName = newName;
                _configCenterEnvironmentUnitOfWork.RegisterEdit(configurationItem);
            }
            await _configCenterEnvironmentUnitOfWork.CommitAsync();
            await _configurationItemRepository.ClearCacheAsync();
        }

        public async Task EditAllNamespaceName(string projectName, string oldName, string newName)
        {
            List<ConfigurationItem> configurationItems = await _configurationItemRepository.FindAsync(m => m.ProjectName == projectName && m.NamespaceName == oldName);
            foreach (ConfigurationItem configurationItem in configurationItems)
            {
                configurationItem.NamespaceName = newName;
                _configCenterEnvironmentUnitOfWork.RegisterEdit(configurationItem);
            }
            await _configCenterEnvironmentUnitOfWork.CommitAsync();
            await _configurationItemRepository.ClearCacheAsync();
        }
    }
}
