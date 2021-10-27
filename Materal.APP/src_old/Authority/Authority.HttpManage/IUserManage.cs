using Authority.DataTransmitModel.User;
using Authority.HttpManage.Models;
using Authority.PresentationModel.User;
using Materal.Model;
using System;
using System.Threading.Tasks;

namespace Authority.HttpManage
{
    public interface IUserManage
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> AddUserAsync(AddUserRequestModel requestModel);

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> EditUserAsync(EditUserRequestModel requestModel);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel> DeleteUserAsync(Guid id);

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel<UserDTO>> GetUserInfoAsync(Guid id);

        /// <summary>
        /// 获取我的信息
        /// </summary>
        /// <returns></returns>
        Task<ResultModel<UserDTO>> GetMyUserInfoAsync();

        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<PageResultModel<UserListDTO>> GetUserListAsync(QueryUserFilterRequestModel requestModel);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel<LoginResultModel>> LoginAsync(LoginRequestModel requestModel);

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResultModel<string>> ResetPasswordAsync(Guid id);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel);
    }
}
