using ConfigCenter.Environment.Common;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigCenter.Environment.Services.Models.ConfigurationItem;

namespace ConfigCenter.Environment.Services
{
    public interface IConfigurationItemService
    {
        /// <summary>
        /// 初始化配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task InitConfigurationItemsAsync(ICollection<AddConfigurationItemModel> model);
        /// <summary>
        /// 添加配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task AddConfigurationItemAsync(AddConfigurationItemModel model);
        /// <summary>
        /// 修改配置项
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task EditConfigurationItemAsync(EditConfigurationItemModel model);
        /// <summary>
        /// 删除配置项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task DeleteConfigurationItemAsync(Guid id);
        /// <summary>
        /// 根据项目ID删除配置项
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task DeleteConfigurationItemByProjectIDAsync(Guid projectID);
        /// <summary>
        /// 根据命名空间删除配置项
        /// </summary>
        /// <param name="namespaceID"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task DeleteConfigurationItemByNamespaceIDAsync(Guid namespaceID);
        /// <summary>
        /// 获得配置项信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task<ConfigurationItemDTO> GetConfigurationItemInfoAsync(Guid id);
        /// <summary>
        /// 获得配置项列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task<List<ConfigurationItemListDTO>> GetConfigurationItemListAsync(QueryConfigurationItemFilterModel filterModel);
    }
}
