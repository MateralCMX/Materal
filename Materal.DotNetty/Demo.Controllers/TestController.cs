using Materal.DotNetty.ControllerBus;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.DotNetty.Server.Core.Models;

namespace Demo.Controllers
{
    public class TestController : BaseController
    {
        [HttpGet]
        public string Test01()
        {
            return "OK";
        }
        [HttpPost]
        public string UploadFile(IUploadFileModel file)
        {
            return "OK";
        }
    }
}
