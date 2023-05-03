#nullable enable
using RC.Core.HttpClient;
using Materal.Utils.Model;
using AspectCore.DynamicProxy;
using Materal.BaseCore.Common;
using Materal.BaseCore.PresentationModel;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.PresentationModel.User;
using System.ComponentModel.DataAnnotations;

namespace RC.Authority.HttpClient
{
    public partial class UserHttpClient : HttpClientBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
        public UserHttpClient() : base("RC.Authority") { }
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
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task ChangePasswordAsync(ChangePasswordRequestModel requestModel) => await GetResultModelByPutAsync("User/ChangePassword", null, requestModel);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string?> ResetPasswordAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByPutAsync<string>("User/ResetPassword", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
    }
}
