using Authority.DataTransmitModel.WebMenuAuthority;
using Authority.Service.Model.WebMenuAuthority;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Authority.Service
{
    /// <summary>
    /// 网页菜单权限服务
    /// </summary>
    public interface IWebMenuAuthorityService
    {
        /// <summary>
        /// 添加网页菜单权限
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddWebMenuAuthorityAsync(AddWebMenuAuthorityModel model);
        /// <summary>
        /// 修改网页菜单权限
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task EditWebMenuAuthorityAsync(EditWebMenuAuthorityModel model);
        /// <summary>
        /// 删除网页菜单权限
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task DeleteWebMenuAuthorityAsync(Guid id);
        /// <summary>
        /// 获得网页菜单权限信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<WebMenuAuthorityDTO> GetWebMenuAuthorityInfoAsync(Guid id);
        /// <summary>
        /// 获得网页菜单权限树形
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<List<WebMenuAuthorityTreeDTO>> GetWebMenuAuthorityTreeAsync();
        /// <summary>
        /// 获得用户拥有的网页菜单权限树形
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<List<WebMenuAuthorityTreeDTO>> GetWebMenuAuthorityTreeAsync(Guid userID);
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="targetID"></param>
        /// <param name="forUnder"></param>
        /// <returns></returns>
        Task ExchangeWebMenuAuthorityIndexAsync(Guid exchangeID, Guid targetID, bool forUnder = true);
        /// <summary>
        /// 更换父级
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <param name="targetID">位序目标唯一标识</param>
        /// <param name="forUnder">在位序目标之下</param>
        /// <returns></returns>
        Task ExchangeWebMenuAuthorityParentIDAsync(Guid id, Guid? parentID, Guid? targetID, bool forUnder = true);
    }
}
