using Materal.Utils;
using Materal.Utils.Consul;
using Microsoft.AspNetCore.Mvc;

namespace Materal.WebAPITest.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HealthController(IConsulService consulService) : ControllerBase
    {
        [HttpGet]
        public IActionResult Health(Guid? id)
        {
            ConsoleQueue.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]Consul->Local健康检测：{(id is null ? "无节点" : id.Value)}");
            //return NotFound();
            if (id is null || consulService.HasNode(id.Value)) return Ok();
            throw new Exception("节点ID不匹配");
        }
    }
}
