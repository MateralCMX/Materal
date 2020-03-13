using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.ControllerBus.Attributes;

namespace Demo.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet]
        public string Test01()
        {
            return "OK";
        }
    }
}
