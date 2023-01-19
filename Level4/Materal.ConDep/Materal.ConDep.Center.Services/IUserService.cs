using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.ConDep.Center.DataTransmitModel.User;
using Materal.ConDep.Center.Services.Models.User;
using Materal.Model;

namespace Materal.ConDep.Center.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConDepException"></exception>
        [DataValidation]
        Task AddUserAsync(AddUserModel model);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConDepException"></exception>
        [DataValidation]
        Task EditUserAsync(EditUserModel model);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralConDepException"></exception>
        [DataValidation]
        Task DeleteUserAsync(Guid id);
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralConDepException"></exception>
        [DataValidation]
        Task<UserDTO> GetUserInfoAsync(Guid id);
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="MateralConDepException"></exception>
        [DataValidation]
        Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel filterModel);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConDepException"></exception>
        [DataValidation]
        Task<UserDTO> LoginAsync(LoginModel model);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralConDepException"></exception>
        [DataValidation]
        Task<string> ResetPasswordAsync(Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConDepException"></exception>
        [DataValidation]
        Task ChangePasswordAsync(ChangePasswordModel model);
    }
}
