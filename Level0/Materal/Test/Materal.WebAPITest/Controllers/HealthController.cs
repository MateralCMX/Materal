//using Materal.Utils.Consul;
//using Microsoft.AspNetCore.Mvc;

//namespace Materal.WebAPITest.Controllers
//{
//    [ApiController]
//    [Route("/api/[controller]")]
//    public class HealthController(IConsulService consulService) : ControllerBase
//    {
//        [HttpGet]
//        public void Health(Guid? id)
//        {
//            if (id is null || consulService.HasNode(id.Value)) return;
//            throw new Exception("节点ID不匹配");
//        }
//    }
//}
