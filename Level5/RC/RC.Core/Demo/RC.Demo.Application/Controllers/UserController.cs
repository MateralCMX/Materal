using RC.Demo.Abstractions.DTO.User;
using RC.Demo.Abstractions.RequestModel.User;
using RC.Demo.Abstractions.Services.Models.User;

namespace RC.Demo.Application.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public partial class UserController(ITokenService tokenService, IOptionsMonitor<AuthorizationConfig> authorizationConfig)
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<string>> ResetPasswordAsync([Required(ErrorMessage = "id    Ϊ  ")] Guid id)
        {
            string result = await DefaultService.ResetPasswordAsync(id);
            return ResultModel<string>.Success(result);
        }
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetLoginUserInfoAsync()
        {
            Guid id = this.GetLoginUserID();
            UserDTO result = await DefaultService.GetInfoAsync(id);
            return ResultModel<UserDTO>.Success(result, "  ѯ ɹ ");
        }
        /// <summary>
        /// 登录
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
                return ResultModel<LoginResultDTO>.Success(result, "  ¼ ɹ ");
            }
            catch (RCException)
            {
                return ResultModel<LoginResultDTO>.Fail(" ˺Ż          ");
            }
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel> ChangePasswordAsync(ChangePasswordRequestModel requestModel)
        {
            ChangePasswordModel model = Mapper.Map<ChangePasswordModel>(requestModel);
            BindLoginUserID(model);
            await DefaultService.ChangePasswordAsync(model);
            return ResultModel.Success(" ޸ĳɹ ");
        }
    }
}