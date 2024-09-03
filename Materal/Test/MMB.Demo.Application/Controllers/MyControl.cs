using Materal.MergeBlock.Authorization.Abstractions;
using Materal.MergeBlock.Web.Abstractions.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MMB.Demo.Application.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class MyController(ITokenService tokenService) : ControllerBase, IMergeBlockController
    {
        [HttpGet]
        public string Hello(string name) => $"Hello {name}!";
        [HttpGet, AllowAnonymous]
        public string GetToken() => tokenService.GetToken(Guid.NewGuid());
    }
}
