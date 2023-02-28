using MBC.Core.HttpClient;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.PresentationModel.User;
using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.HttpClient
{
    public class UserHttpClient : HttpClientBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
        public UserHttpClient() : base("MBC.Demo") { }
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserDTO?> GetLoginUserInfoAsync() => await GetResultModelByGetAsync<UserDTO>($"User/GetLoginUserInfo", null);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<LoginResultDTO?> LoginAsync(LoginRequestModel requestModel) => await GetResultModelByPostAsync<LoginResultDTO>($"User/Login", null, requestModel);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string?> ResetPasswordAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id) => await GetResultModelByPutAsync<string>($"User/ResetPassword", new Dictionary<string, string> { [nameof(id)] = id.ToString() });
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task ChangePasswordAsync(ChangePasswordRequestModel requestModel) => await GetResultModelByPutAsync<string>($"User/ChangePassword", null, requestModel);
    }
}
