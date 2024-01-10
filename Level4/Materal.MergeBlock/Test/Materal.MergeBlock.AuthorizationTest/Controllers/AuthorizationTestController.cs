using Materal.MergeBlock.Application.Controllers;
using Materal.MergeBlock.Authorization.Abstractions;
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
