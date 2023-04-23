using Materal.BaseCore.CodeGenerator;
using Materal.Utils.Model;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.Services.Models.User;

namespace MBC.Demo.Services
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
        [DataValidation, MapperController(MapperType.Put)]
        Task<string> ResetPasswordAsync(Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        [DataValidation, MapperController(MapperType.Post)]
        Task TestChangePasswordAsync(ChangePasswordModel model);
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
