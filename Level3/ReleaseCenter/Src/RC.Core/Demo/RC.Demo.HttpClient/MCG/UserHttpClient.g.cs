#nullable enable
using RC.Core.HttpClient;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.PresentationModel.User;
using System.ComponentModel.DataAnnotations;

namespace RC.Demo.HttpClient
{
    public partial class UserHttpClient : HttpClientBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
        public UserHttpClient() : base("RC.Demo") { }
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<UserDTO?> GetLoginUserInfoAsync() => await GetResultModelByGetAsync<UserDTO>("User/GetLoginUserInfo", null, null);
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<LoginResultDTO?> LoginAsync(LoginRequestModel requestModel) => await GetResultModelByPostAsync<LoginResultDTO>("User/Login", requestModel, null);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string?> ResetPasswordAsync([Required(ErrorMessage = "唯一标识不能为空")] Guid id) => await GetResultModelByPutAsync<string>("User/ResetPassword", null, new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task ChangePasswordAsync(ChangePasswordRequestModel requestModel) => await GetResultModelByPutAsync("User/ChangePassword", requestModel, null);
        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<List<UserDTO>?> TestAsync(ChangePasswordRequestModel requestModel) => await GetResultModelByPutAsync<List<UserDTO>>("User/Test", requestModel, null);
    }
}
