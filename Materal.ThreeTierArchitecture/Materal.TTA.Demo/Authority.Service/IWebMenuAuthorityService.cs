using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Common;
using Authority.DataTransmitModel.WebMenuAuthority;
using Authority.Service.Model.WebMenuAuthority;
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
        /// 获得网页菜单权限列表
        /// </summary>
        /// <param name="filterModel">查询模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<(List<WebMenuAuthorityListDTO> result, PageModel pageModel)> GetWebMenuAuthorityListAsync(QueryWebMenuAuthorityFilterModel filterModel);
    }
}
