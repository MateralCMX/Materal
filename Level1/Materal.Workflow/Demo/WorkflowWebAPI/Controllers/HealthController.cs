using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowWebAPI.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("api/[controller]"), ApiController, AllowAnonymous]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="id">节点ID</param>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult Index()
        {
            return Ok();
        }
    }
}
