using Materal.ConDep.Controllers.Models;
using Materal.ConDep.Services;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System;
using Materal.ConDep.ControllerCore;

namespace Materal.ConDep.Controllers
{
    public class AuthorityController : ConDepBaseController
    {
        private readonly IAuthorityService _authorityService;
        public AuthorityController(IAuthorityService authorityService)
        {
            _authorityService = authorityService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAuthority]
        public ResultModel<string> Login(LoginRequestModel requestModel)
        {
            try
            {
                string token = _authorityService.Login(requestModel.Password);
                return ResultModel<string>.Success(token, "登录成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<string>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAuthority]
        public ResultModel Logout()
        {
            try
            {
                _authorityService.Logout(GetToken());
                return ResultModel.Success("登出成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
