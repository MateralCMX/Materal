using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Common;
using Authority.DataTransmitModel.APIAuthority;
using Authority.Service.Model.APIAuthority;
namespace Authority.Service
{
    /// <summary>
    /// API权限服务
    /// </summary>
    public interface IAPIAuthorityService
    {
        /// <summary>
        /// 添加API权限
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddAPIAuthorityAsync(AddAPIAuthorityModel model);
        /// <summary>
        /// 修改API权限
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task EditAPIAuthorityAsync(EditAPIAuthorityModel model);
        /// <summary>
        /// 删除API权限
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task DeleteAPIAuthorityAsync(Guid id);
        /// <summary>
        /// 获得API权限信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<APIAuthorityDTO> GetAPIAuthorityInfoAsync(Guid id);
        /// <summary>
        /// 获得API权限树形
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<List<APIAuthorityTreeDTO>> GetAPIAuthorityTreeAsync();
        /// <summary>
        /// 更换父级
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <param name="parentID">父级唯一标识</param>
        /// <returns></returns>
        Task ExchangeAPIAuthorityParentIDAsync(Guid id, Guid? parentID);
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <param name="codes">权限代码</param>
        /// <returns></returns>
        Task<bool> HasAPIAuthorityAsync(Guid userID, params string[] codes);
        /// <summary>
        /// 验证登录权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<bool> HasLoginAuthorityAsync(Guid userID);
    }
}
