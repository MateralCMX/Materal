#nullable enable
using MBC.Core.HttpClient;
using AspectCore.DynamicProxy;
using Materal.BaseCore.Common;
using Materal.BaseCore.PresentationModel;
using Materal.Utils.Model;
using MBC.Core.Common;
using MBC.Demo.Common;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.PresentationModel.User;
using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.HttpClient
{
    public partial class UserHttpClient : HttpClientBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
        public UserHttpClient() : base("MBC.Demo") { }
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserDTO?> GetLoginUserInfoAsync() => await GetResultModelByGetAsync<UserDTO>("User/GetLoginUserInfo");
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<LoginResultDTO?> LoginAsync(LoginRequestModel requestModel) => await GetResultModelByPostAsync<LoginResultDTO>("User/Login", null, requestModel);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string?> ResetPasswordAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id) => await GetResultModelByPutAsync<string>("User/ResetPassword", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task ChangePasswordAsync(ChangePasswordRequestModel requestModel) => await GetResultModelByPutAsync("User/ChangePassword", null, requestModel);
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<List<UserDTO>?> TestAsync(ChangePasswordRequestModel requestModel) => await GetResultModelByPutAsync<List<UserDTO>>("User/Test", null, requestModel);
    }
}