using AutoMapper;
using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.Domain;
using Materal.ConfigCenter.ConfigServer.Domain.Repositories;
using Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.Services;
using Materal.ConfigCenter.ConfigServer.SqliteEFRepository;
using Materal.ConvertHelper;
using Materal.LinqHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ConfigServer.ServiceImpl
{
    public class ConfigurationItemServiceImpl : IConfigurationItemService
    {
        private readonly IConfigurationItemRepository _configurationItemRepository;
        private readonly IConfigServerUnitOfWork _protalServerUnitOfWork;
        private readonly IMapper _mapper;

        public ConfigurationItemServiceImpl(IConfigurationItemRepository configurationItemRepository, IConfigServerUnitOfWork protalServerUnitOfWork, IMapper mapper)
        {
            _configurationItemRepository = configurationItemRepository;
            _protalServerUnitOfWork = protalServerUnitOfWork;
            _mapper = mapper;
        }
        public async Task InitConfigurationItemsAsync(List<AddConfigurationItemModel> model)
        {
            List<ConfigurationItem> allConfigurationItems = await _configurationItemRepository.FindAsync(m => true);
            foreach (ConfigurationItem configurationItem in allConfigurationItems)
            {
                _protalServerUnitOfWork.RegisterDelete(configurationItem);
            }
            foreach (AddConfigurationItemModel itemModel in model)
            {
                var configurationItem = itemModel.CopyProperties<ConfigurationItem>();
                _protalServerUnitOfWork.RegisterAdd(configurationItem);
            }
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task InitConfigurationItemsByNamespaceAsync(InitConfigurationItemsByNamespaceModel model)
        {
            List<ConfigurationItem> allConfigurationItems = await _configurationItemRepository.FindAsync(m => m.NamespaceID == model.NamespaceID);
            foreach (ConfigurationItem configurationItem in allConfigurationItems)
            {
                _protalServerUnitOfWork.RegisterDelete(configurationItem);
            }
            foreach (AddConfigurationItemModel itemModel in model.ConfigurationItems)
            {
                var configurationItem = itemModel.CopyProperties<ConfigurationItem>();
                _protalServerUnitOfWork.RegisterAdd(configurationItem);
            }
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task AddConfigurationItemAsync(AddConfigurationItemModel model)
        {
            if (await _configurationItemRepository.ExistedAsync(m => m.Key.Equals(model.Key) && m.NamespaceID == model.NamespaceID && m.ProjectID == model.ProjectID)) throw new MateralConfigCenterException("Key已存在");
            var configurationItem = model.CopyProperties<ConfigurationItem>();
            _protalServerUnitOfWork.RegisterAdd(configurationItem);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task EditConfigurationItemAsync(EditConfigurationItemModel model)
        {
            if (await _configurationItemRepository.ExistedAsync(m => m.Key.Equals(model.Key) && m.NamespaceID == model.NamespaceID && m.ProjectID == model.ProjectID && m.ID != model.ID)) throw new MateralConfigCenterException("Key已存在");
            ConfigurationItem configurationItemFromDb = await _configurationItemRepository.FirstOrDefaultAsync(model.ID);
            if (configurationItemFromDb == null) throw new MateralConfigCenterException("配置项不存在");
            model.CopyProperties(configurationItemFromDb);
            configurationItemFromDb.UpdateTime = DateTime.Now;
            _protalServerUnitOfWork.RegisterEdit(configurationItemFromDb);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task DeleteConfigurationItemAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            ConfigurationItem configurationItemFromDb = await _configurationItemRepository.FirstOrDefaultAsync(id);
            if (configurationItemFromDb == null) throw new MateralConfigCenterException("配置项不存在");
            _protalServerUnitOfWork.RegisterDelete(configurationItemFromDb);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task DeleteConfigurationItemByProjectIDAsync(Guid projectID)
        {
            List<ConfigurationItem> configurationItems = await _configurationItemRepository.FindAsync(m => m.ProjectID == projectID);
            foreach (ConfigurationItem configurationItem in configurationItems)
            {
                _protalServerUnitOfWork.RegisterDelete(configurationItem);
            }
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task DeleteConfigurationItemByNamespaceIDAsync(Guid namespaceID)
        {
            List<ConfigurationItem> configurationItems = await _configurationItemRepository.FindAsync(m => m.NamespaceID == namespaceID);
            foreach (ConfigurationItem configurationItem in configurationItems)
            {
                _protalServerUnitOfWork.RegisterDelete(configurationItem);
            }
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task<ConfigurationItemDTO> GetConfigurationItemInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            ConfigurationItem configurationItemFromDb = await _configurationItemRepository.FirstOrDefaultAsync(id);
            if (configurationItemFromDb == null) throw new MateralConfigCenterException("配置项不存在");
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
                    if (temp == null)
                    {
                        temp = m => m.NamespaceName.Equals(namespaceName);
                    }
                    else
                    {
                        temp = temp.Or(m => m.NamespaceName.Equals(namespaceName));
                    }
                }
                searchExpression = searchExpression.And(temp);
            }
            List<ConfigurationItem> configurationItemsFromDb = await _configurationItemRepository.FindAsync(searchExpression, m => m.Key, SortOrder.Ascending);
            var result = _mapper.Map<List<ConfigurationItemListDTO>>(configurationItemsFromDb);
            return result;
        }
    }
}
