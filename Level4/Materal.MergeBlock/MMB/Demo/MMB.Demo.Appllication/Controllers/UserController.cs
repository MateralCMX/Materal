using Microsoft.Extensions.Options;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.RequestModel.User;
using MMB.Demo.Abstractions.Services.Models.User;
using System.ComponentModel.DataAnnotations;

namespace MMB.Demo.Appllication.Controllers
{
    /// <summary>
    /// ���Կ�����
    /// </summary>
    public class UserController(ITokenService tokenService, IOptionsMonitor<AuthorizationConfig> authorizationConfig) : MergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>, IUserController
    {
        /// <summary>
        /// ����û��б�
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResultModel<UserListDTO>> GetUserListAsync(QueryUserRequestModel requestModel)
        {
            QueryUserModel model0 = Mapper.Map<QueryUserModel>(requestModel);
            (List<UserListDTO>? result, PageModel pageInfo) = await DefaultService.GetUserListAsync(model0);
            return PageResultModel<UserListDTO>.Success(result, pageInfo);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<string>> ResetPasswordAsync([Required(ErrorMessage = "id����Ϊ��")] Guid id)
        {
            string result = await DefaultService.ResetPasswordAsync(id);
            return ResultModel<string>.Success(result);
        }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> TestChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            ChangePasswordModel model0 = Mapper.Map<ChangePasswordModel>(requestModel);
            await DefaultService.TestChangePasswordAsync(model0);
            return ResultModel.Success();
        }
        /// <summary>
        /// ��õ�¼�û���Ϣ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetLoginUserInfoAsync()
        {
            Guid id = this.GetLoginUserID();
            UserDTO result = await DefaultService.GetInfoAsync(id);
            return ResultModel<UserDTO>.Success(result, "��ѯ�ɹ�");
        }
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<ResultModel<LoginResultDTO>> LoginAsync(LoginRequestModel requestModel)
        {
            try
            {
                LoginModel model = Mapper.Map<LoginModel>(requestModel);
                UserDTO userInfo = await DefaultService.LoginAsync(model);
                string token = tokenService.GetToken(userInfo.ID);
                LoginResultDTO result = new()
                {
                    Token = token,
                    ExpiredTime = authorizationConfig.CurrentValue.ExpiredTime
                };
                return ResultModel<LoginResultDTO>.Success(result, "��¼�ɹ�");
            }
            catch (MMBException)
            {
                return ResultModel<LoginResultDTO>.Fail("�˺Ż����������");
            }
        }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            ChangePasswordModel model = Mapper.Map<ChangePasswordModel>(requestModel);
            model.ID = this.GetLoginUserID();
            await DefaultService.ChangePasswordAsync(model);
            return ResultModel.Success("�޸ĳɹ�");
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public Task<ResultModel<List<UserDTO>>> TestAsync(ChangePasswordRequestModel requestModel)
        {
            throw new NotImplementedException();
        }
    }
}
