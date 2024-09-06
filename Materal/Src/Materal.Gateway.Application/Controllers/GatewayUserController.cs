using Materal.Gateway.Common;
using Materal.MergeBlock.Authorization.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Materal.Gateway.Application.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class GatewayUserController(IOptionsMonitor<ApplicationConfig> config, ITokenService tokenService, IOptionsMonitor<MergeBlock.Authorization.Abstractions.AuthorizationOptions> authorizationConfig) : GatewayControllerBase
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ResultModel<LoginResultDTO> Login(LoginRequestModel requestModel)
        {
            try
            {
                UserEntity user = config.CurrentValue.Users.FirstOrDefault(m => m.Account == requestModel.Account && m.Password == requestModel.Password) ?? throw new GatewayException("用户名或密码错误");
                string token = tokenService.GetToken(Guid.NewGuid());
                var result = new LoginResultDTO
                {
                    Token = token,
                    ExpiredTime = authorizationConfig.CurrentValue.ExpiredTime
                };
                return ResultModel<LoginResultDTO>.Success(result, "登录成功");
            }
            catch (GatewayException)
            {
                return ResultModel<LoginResultDTO>.Fail("用户名或密码错误");
            }
        }
    }
}
