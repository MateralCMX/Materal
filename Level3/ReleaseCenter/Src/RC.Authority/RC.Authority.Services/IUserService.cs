using Materal.Utils.Model;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.Services.Models.User;
using RC.Core.Common;

namespace RC.Authority.Services
{
    public partial interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        [DataValidation]
        Task<UserDTO> LoginAsync(LoginModel model);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        [DataValidation]
        Task<string> ResetPasswordAsync(Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        [DataValidation]
        Task ChangePasswordAsync(ChangePasswordModel model);
        /// <summary>
        /// 添加默认用户
        /// </summary>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        Task AddDefaultUserAsync();
    }
}
