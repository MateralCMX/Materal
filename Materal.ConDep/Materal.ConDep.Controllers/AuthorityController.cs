using Materal.ConDep.Authority;
using Materal.ConDep.Controllers.Models.Authority;
using Materal.Model;
using Materal.WebSocket.Http.Attributes;
using System;

namespace Materal.ConDep.Controllers
{
    public class AuthorityController
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
        [HttpPost, AllowAnonymous]
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
    }
}
