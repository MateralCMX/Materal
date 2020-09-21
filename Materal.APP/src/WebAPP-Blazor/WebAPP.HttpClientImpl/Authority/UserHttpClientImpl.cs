using Authority.DataTransmitModel.User;
using Authority.HttpManage;
using Authority.HttpManage.Models;
using Authority.PresentationModel.User;
using Materal.APP.HttpClient;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPP.HttpClientImpl.Authority
{
    public class UserHttpClientImpl : AuthorityHttpClient, IUserManage
    {
        private const string _controllerUrl = "/api/User/";
        public UserHttpClientImpl(IAuthorityManage authorityManage) : base(authorityManage)
        {
        }

        public async Task<ResultModel> AddUserAsync(AddUserRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}AddUser", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> EditUserAsync(EditUserRequestModel requestModel)
        {
            var resultModel = await SendPutAsync<ResultModel>($"{_controllerUrl}EditUser", requestModel);
            return resultModel;
        }

        public async Task<ResultModel> DeleteUserAsync(Guid id)
        {
            var resultModel = await SendDeleteAsync<ResultModel>($"{_controllerUrl}DeleteUser", null, new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<UserDTO>> GetUserInfoAsync(Guid id)
        {
            var resultModel = await SendGetAsync<ResultModel<UserDTO>>($"{_controllerUrl}GetUserInfo", new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel<UserDTO>> GetMyUserInfoAsync()
        {
            var resultModel = await SendGetAsync<ResultModel<UserDTO>>($"{_controllerUrl}GetMyUserInfo");
            return resultModel;
        }

        public async Task<PageResultModel<UserListDTO>> GetUserListAsync(QueryUserFilterRequestModel requestModel)
        {
            var resultModel = await SendPostAsync<PageResultModel<UserListDTO>>($"{_controllerUrl}GetUserList", requestModel);
            return resultModel;
        }

        public async Task<ResultModel<LoginResultModel>> LoginAsync(LoginRequestModel requestModel)
        {
            var resultModel = await SendPostAsync<ResultModel<LoginResultModel>>($"{_controllerUrl}Login", requestModel);
            return resultModel;
        }

        public async Task<ResultModel<string>> ResetPasswordAsync(Guid id)
        {
            var resultModel = await SendPostAsync<ResultModel<string>>($"{_controllerUrl}ResetPassword", null, new Dictionary<string, string>
            {
                ["id"] = id.ToString()
            });
            return resultModel;
        }

        public async Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            var resultModel = await SendPostAsync<ResultModel>($"{_controllerUrl}ChangePassword", requestModel);
            return resultModel;
        }
    }
}
