using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Common;
using Authority.DataTransmitModel.User;
using Authority.Service.Model.User;
namespace Authority.Service
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model">添加模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddUserAsync(AddUserModel model);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model">修改模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task EditUserAsync(EditUserModel model);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task DeleteUserAsync(Guid id);
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<UserDTO> GetUserInfoAsync(Guid id);
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="filterModel">查询模型</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel filterModel);
    }
}
