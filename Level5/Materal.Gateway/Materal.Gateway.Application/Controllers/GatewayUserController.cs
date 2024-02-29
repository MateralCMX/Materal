using Materal.Gateway.Common;
using Materal.Gateway.Controllers;
using Materal.MergeBlock.Abstractions.WebModule.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Materal.Gateway.Application.Controllers
{
    /// <summary>
    /// �û�������
    /// </summary>
    public class GatewayUserController(IOptionsMonitor<ApplicationConfig> config, ITokenService tokenService, IOptionsMonitor<AuthorizationConfig> authorizationConfig) : GatewayControllerBase
    {
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ResultModel<LoginResultDTO> Login(LoginRequestModel requestModel)
        {
            try
            {
                UserEntity user = config.CurrentValue.Users.FirstOrDefault(m => m.Account == requestModel.Account && m.Password == requestModel.Password) ?? throw new GatewayException("�û������������");
                string token = tokenService.GetToken(Guid.NewGuid());
                var result = new LoginResultDTO
                {
                    Token = token,
                    ExpiredTime = authorizationConfig.CurrentValue.ExpiredTime
                };
                return ResultModel<LoginResultDTO>.Success(result, "��¼�ɹ�");
            }
            catch (GatewayException)
            {
                return ResultModel<LoginResultDTO>.Fail("�û������������");
            }
        }
    }
}
