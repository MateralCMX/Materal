#nullable enable
using RC.Core.HttpClient;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.PresentationModel.User;
using System.ComponentModel.DataAnnotations;
using Materal.BaseCore.Common.Utils.TreeHelper;
using Materal.BaseCore.Common.Utils.IndexHelper;
using Materal.BaseCore.PresentationModel;
using Materal.Utils.Model;

namespace RC.Demo.HttpClient
{
    public partial class UserHttpClient : HttpClientBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
        public UserHttpClient() : base("RC.Demo") { }
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
