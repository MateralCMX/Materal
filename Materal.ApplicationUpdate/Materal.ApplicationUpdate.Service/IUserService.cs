using Materal.ApplicationUpdate.Common;
using Materal.ApplicationUpdate.DTO.User;
using Materal.ApplicationUpdate.Service.Model.User;
using Materal.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ApplicationUpdate.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="addUserModel">添加对象</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task AddUserAsync(AddUserModel addUserModel);
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="editUserModel">修改对象</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApplicationUpdateException"></exception>
        Task EditUserAsync(EditUserModel editUserModel);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <returns></returns>
        /// <exception cref="ApplicationUpdateException"></exception>
        Task DeleteUserAsync(Guid userID);
        /// <summary>
        /// 获取用户列表信息
        /// </summary>
        /// <param name="userListFilter"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(UserListFilter userListFilter);
        /// <summary>
        /// 根据唯一标识获取用户信息
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <returns>用户信息</returns>
        /// <exception cref="ApplicationUpdateException"></exception>
        Task<UserDTO> GetUserInfoByIDAsync(Guid userID);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userLoginModel">登录模型</param>
        /// <returns>登录用户信息</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ApplicationUpdateException"></exception>
        Task<LoginUserDTO> LoginAsync(UserLoginModel userLoginModel);
        /// <summary>
        /// 根据Token获取信息
        /// </summary>
        /// <param name="token">Token值</param>
        /// <returns>用户信息</returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task<LoginUserDTO> GetUserInfoByTokenAsync(string token);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="changePasswordModel">修改密码模型</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ApplicationUpdateException"></exception>
        Task ChangePasswordAsync(ChangePasswordModel changePasswordModel);
    }
}
