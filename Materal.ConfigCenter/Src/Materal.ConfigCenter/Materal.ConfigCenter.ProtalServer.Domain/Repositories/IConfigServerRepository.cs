using Materal.ConfigCenter.ConfigServer.DataTransmitModel.ConfigurationItem;
using Materal.ConfigCenter.ConfigServer.PresentationModel.ConfigurationItem;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.Domain.Repositories
{
    public interface IConfigServerRepository
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task HealthAsync(string address);
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="address"></param>
        /// <param name="token"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProjectAsync(string address, string token, Guid id);
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="address"></param>
        /// <param name="token"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteNamespaceAsync(string address, string token, Guid id);
        /// <summary>
        /// 获得所有配置项
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<List<ConfigurationItemListDTO>> GetConfigurationItemAsync(QueryConfigurationItemFilterModel filterModel, string address);
        /// <summary>
        /// 初始化配置项
        /// </summary>
        /// <param name="model"></param>
        /// <param name="address"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task InitConfigurationItemsAsync(string address, string token, List<AddConfigurationItemModel> model);
        /// <summary>
        /// 初始化配置项
        /// </summary>
        /// <param name="model"></param>
        /// <param name="address"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task InitConfigurationItemsByNamespaceAsync(string address, string token, InitConfigurationItemsByNamespaceModel model);
    }
}
