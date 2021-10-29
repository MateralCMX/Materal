using ConfigCenter.Environment.Common;
using ConfigCenter.Environment.DataTransmitModel.ConfigurationItem;
using ConfigCenter.Environment.Services.Models.ConfigurationItem;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task DeleteConfigurationItemByProjectAsync(string projectName);
        /// <summary>
        /// 根据命名空间删除配置项
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="namespaceName"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task DeleteConfigurationItemByNamespaceAsync(string projectName, string namespaceName);
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
        /// <summary>
        /// 更改所有项目名称
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task EditAllProjectName(string oldName, string newName);
        /// <summary>
        /// 更改所有命名空间名称
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        /// <exception cref="ConfigCenterEnvironmentException"></exception>
        [DataValidation]
        Task EditAllNamespaceName(string projectName, string oldName, string newName);
    }
}
