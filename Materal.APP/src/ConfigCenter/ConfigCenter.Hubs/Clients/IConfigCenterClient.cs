using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigCenter.Environment.PresentationModel.ConfigurationItem;

namespace ConfigCenter.Hubs.Clients
{
    public interface IConfigCenterClient
    {
        /// <summary>
        /// 注册结果
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task RegisterResult(bool isSuccess, string message);

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProject(Guid id);

        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteNamespace(Guid id);
        /// <summary>
        /// 同步配置项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="targetKeys"></param>
        /// <param name="configurationItems"></param>
        /// <returns></returns>
        Task SyncConfigurationItem(string key, ICollection<string> targetKeys, ICollection<AddConfigurationItemRequestModel> configurationItems);
    }
}
