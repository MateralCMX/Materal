using Materal.Utils.Consul;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Materal.MergeBlock.Consul
{
    [ApiController, Route("/api/[controller]")]
    public class HealthController(IConsulService consulService) : ControllerBase
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public virtual IActionResult Get(Guid? id)
        {
            if(id is not null && id != consulService.NodeID) return NotFound();
            return Ok();
        }
    }
}
