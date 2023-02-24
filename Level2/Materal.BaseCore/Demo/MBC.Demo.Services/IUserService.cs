using Materal.BaseCore.Services;
using Materal.Utils.Model;
using MBC.Core.Common;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.Services.Models.User;

namespace MBC.Demo.Services
{
    public interface IUserService : IBaseService<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MBCException"></exception>
        [DataValidation]
        Task<UserDTO> LoginAsync(LoginModel model);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MBCException"></exception>
        [DataValidation]
        Task<string> ResetPasswordAsync(Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MBCException"></exception>
        [DataValidation]
        Task ChangePasswordAsync(ChangePasswordModel model);
        /// <summary>
        /// 添加默认用户
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MBCException"></exception>
        Task AddDefaultUserAsync();
    }
}
