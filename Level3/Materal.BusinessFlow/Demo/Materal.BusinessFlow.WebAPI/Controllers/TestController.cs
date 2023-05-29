using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.BusinessFlow.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ResultModel<string> HellowWorld()
        {
            return ResultModel<string>.Success("Hello World!");
        }
    }
}
