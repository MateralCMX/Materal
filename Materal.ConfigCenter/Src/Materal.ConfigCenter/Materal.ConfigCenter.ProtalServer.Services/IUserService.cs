using Materal.ConfigCenter.ProtalServer.DataTransmitModel.User;
using Materal.ConfigCenter.ProtalServer.PresentationModel.User;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task AddUserAsync(AddUserModel model);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task EditUserAsync(EditUserModel model);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task DeleteUserAsync(Guid id);
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task<UserDTO> GetUserInfoAsync(Guid id);
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="filterModel"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel filterModel);
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MateralConfigCenterException"></exception>
        [DataValidation]
        Task<UserDTO> LoginAsync(LoginModel model);
    }
}
