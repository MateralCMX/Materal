using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Common;
using Authority.DataTransmitModel.ActionAuthority;
using Authority.Service.Model.ActionAuthority;
namespace Authority.Service
{
    /// <summary>
    /// 功能权限服务
    /// </summary>
    public interface IActionAuthorityService
    {
        /// <summary>
        /// 添加功能权限
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddActionAuthorityAsync(AddActionAuthorityModel model);
        /// <summary>
        /// 修改功能权限
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task EditActionAuthorityAsync(EditActionAuthorityModel model);
        /// <summary>
        /// 删除功能权限
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task DeleteActionAuthorityAsync(Guid id);
        /// <summary>
        /// 获得功能权限信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<ActionAuthorityDTO> GetActionAuthorityInfoAsync(Guid id);
        /// <summary>
        /// 获得功能权限列表
        /// </summary>
        /// <param name="filterModel">查询模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<(List<ActionAuthorityListDTO> result, PageModel pageModel)> GetActionAuthorityListAsync(QueryActionAuthorityFilterModel filterModel);
        /// <summary>
        /// 获得用户拥有的功能权限列表
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <param name="actionGroupCode">功能组标识</param>
        /// <returns></returns>
        Task<List<ActionAuthorityListDTO>> GetUserOwnedActionAuthorityListAsync(Guid userID, string actionGroupCode);
    }
}
