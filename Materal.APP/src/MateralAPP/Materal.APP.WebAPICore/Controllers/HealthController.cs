using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Materal.APP.WebAPICore.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("api/[controller]"), ApiController, AllowAnonymous]
    public class HealthController : BaseController
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
