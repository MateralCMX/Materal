using Materal.MergeBlock.Abstractions.WebModule.Authorization;
using Materal.MergeBlock.Application.WebModule.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.AuthorizationTest.Controllers
{
    /// <summary>
    /// Authorization���Կ�����
    /// </summary>
    public class AuthorizationTestController(ITokenService tokenService) : MergeBlockControllerBase
    {
        [HttpGet, AllowAnonymous]
        public ResultModel<string> GetToken()
        {
            string token = tokenService.GetToken(Guid.NewGuid());
            return ResultModel<string>.Success($"Bearer {token}", "��ȡ�ɹ�");
        }
    }
}
