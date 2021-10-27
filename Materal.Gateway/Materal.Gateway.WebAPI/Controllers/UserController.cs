using Materal.Gateway.Common;
using Materal.Gateway.Common.Models;
using Materal.Gateway.WebAPI.Models;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    [Route("api/[controller]/[action]"), ApiController, AllowAnonymous]
    public class UserController : BaseController
    {
        [HttpPost]
        public ResultModel<string> Login(LoginModel model)
        {
            return Handler(() =>
            {
                if (model.UserName != ApplicationConfig.AuthorizationConfig.UserName ||
                    model.Password != ApplicationConfig.AuthorizationConfig.Password)
                {
                    throw new GatewayException("用户名或者密码错误");
                }
                string token = ApplicationConfig.AuthorizationConfig.GetAuthorizationToken(model.UserName, model.Password);
                return ResultModel<string>.Success(token, "登录成功");
            });
        }
    }
}
