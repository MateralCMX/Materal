using Materal.MergeBlock.Authorization.Abstractions;
using Microsoft.Extensions.Options;
using RC.Authority.Abstractions.DTO.User;
using RC.Authority.Abstractions.RequestModel.User;
using RC.Authority.Abstractions.Services.Models.User;

namespace RC.Authority.Application.Controllers
{
    /// <summary>
    /// 用户服务控制器
    /// </summary>
    public partial class UserController(ITokenService tokenService, IOptionsMonitor<AuthorizationConfig> config)
    {
        /// <summary>
        /// 获得登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<UserDTO>> GetLoginUserInfoAsync()
        {
            Guid id = this.GetLoginUserID();
            UserDTO result = await DefaultService.GetInfoAsync(id);
            return ResultModel<UserDTO>.Success(result, "查询成功");
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<ResultModel<LoginResultDTO>> LoginAsync(LoginRequestModel requestModel)
        {
            LoginModel model = Mapper.Map<LoginModel>(requestModel);
            UserDTO userInfo = await DefaultService.LoginAsync(model);
            LoginResultDTO result = new()
            {
                Token = tokenService.GetToken(userInfo.ID),
                ExpiredTime = config.CurrentValue.ExpiredTime
            };
            return ResultModel<LoginResultDTO>.Success(result, "登录成功");
        }
    }
}
