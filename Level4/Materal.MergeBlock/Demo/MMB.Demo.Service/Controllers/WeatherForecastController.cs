using Materal.MergeBlock.Authorization.Abstractions;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MMB.Demo.WebAPI.Controllers
{
    [ApiController, Route("/api/[controller]/[action]")]
    public class WeatherForecastController(ITokenService tokenService) : ControllerBase
    {
        [HttpGet]
        public ResultModel SayHello()
        {
            return ResultModel.Success("Hello World!");
        }
        [HttpGet, AllowAnonymous]
        public ResultModel<string> GetToken()
        {
            string token = tokenService.GetToken(Guid.NewGuid());
            return ResultModel<string>.Success(token, "获取成功");
        }
    }
}
