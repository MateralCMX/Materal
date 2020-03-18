using Materal.ConfigCenter.ControllerCore;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;

namespace Materal.ConfigCenter.ProtalServer.Controllers
{
    public class HealthController : ConfigCenterBaseController
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAuthority]
        public ResultModel Health()
        {
            return ResultModel.Success("服务正常");
        }
    }
}
