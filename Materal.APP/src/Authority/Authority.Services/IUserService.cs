using Authority.Common;
using Authority.DataTransmitModel.User;
using Authority.Services.Models.User;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AuthorityException"></exception>
        [DataValidation]
        Task AddUserAsync(AddUserModel model);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AuthorityException"></exception>
        [DataValidation]
        Task EditUserAsync(EditUserModel model);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="AuthorityException"></exception>
        [DataValidation]
        Task DeleteUserAsync(Guid id);
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="AuthorityException"></exception>
        [DataValidation]
        Task<UserDTO> GetUserInfoAsync(Guid id);
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AuthorityException"></exception>
        [DataValidation]
        Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel model);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AuthorityException"></exception>
        [DataValidation]
        Task<UserDTO> LoginAsync(LoginModel model);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="AuthorityException"></exception>
        [DataValidation]
        Task<string> ResetPasswordAsync(Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AuthorityException"></exception>
        [DataValidation]
        Task ChangePasswordAsync(ChangePasswordModel model);
    }
}
