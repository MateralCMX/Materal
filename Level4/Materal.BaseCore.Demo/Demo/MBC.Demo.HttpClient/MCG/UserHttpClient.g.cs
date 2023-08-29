#nullable enable
using MBC.Core.HttpClient;
using Materal.Utils.Model;
using AspectCore.DynamicProxy;
using Materal.BaseCore.Common;
using Materal.BaseCore.PresentationModel;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.PresentationModel.User;
using System.ComponentModel.DataAnnotations;

namespace MBC.Demo.HttpClient
{
    public partial class UserHttpClient : HttpClientBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, UserDTO, UserListDTO>
    {
        public UserHttpClient(IServiceProvider serviceProvider) : base("MBC.Demo", serviceProvider) { }
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
        /// 测试
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<List<UserDTO>?> TestAsync(ChangePasswordRequestModel requestModel) => await GetResultModelByPutAsync<List<UserDTO>>("User/Test", null, requestModel);
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<(List<UserListDTO>? data, PageModel pageInfo)> GetUserListAsync(QueryUserRequestModel requestModel) => await GetPageResultModelByPostAsync<UserListDTO>("User/GetUserList", null, requestModel);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string?> ResetPasswordAsync([Required(ErrorMessage = "id不能为空")] Guid id) => await GetResultModelByPutAsync<string>("User/ResetPassword", new Dictionary<string, string> { [nameof(id)] = id.ToString()});
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task TestChangePasswordAsync(ChangePasswordRequestModel requestModel) => await GetResultModelByPostAsync("User/TestChangePassword", null, requestModel);
    }
}
