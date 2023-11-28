using Materal.Gateway.Common;
using Materal.Gateway.WebAPI.ConfigModel;
using Materal.Gateway.WebAPI.DataTransmitModel;
using Materal.Gateway.WebAPI.PresentationModel;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : GatewayControllerBase
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
                UserConfigModel user = WebAPIConfig.Users.FirstOrDefault(m => m.Account == requestModel.Account && m.Password == requestModel.Password) ?? throw new GatewayException("用户名或密码错误");
                string token = WebAPIConfig.JWTConfig.GetToken(Guid.NewGuid());
                var result = new LoginResultDTO
                {
                    Token = token,
                    ExpiredTime = WebAPIConfig.JWTConfig.ExpiredTime
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
