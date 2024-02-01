using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.RequestModel.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Application.Controllers
{
    /// <summary>
    /// �û�������
    /// </summary>
    public partial class UserController : MergeBlockControllerBase<AddUserRequestModel, EditUserRequestModel, QueryUserRequestModel, AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserService>, IUserController
    {

    }
    /// <summary>
    /// �û�������
    /// </summary>
    public partial class UserController(ITokenService tokenService, IOptionsMonitor<AuthorizationConfig> authorizationConfig)
    {
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
            BindLoginUserID(model);
            await DefaultService.ChangePasswordAsync(model);
            return ResultModel.Success("�޸ĳɹ�");
        }
    }
}
