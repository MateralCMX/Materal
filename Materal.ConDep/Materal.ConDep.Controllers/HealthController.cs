using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;

namespace Materal.ConDep.Controllers
{
    public class HealthController : ConDepBaseController
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAuthority]
        public ResultModel Health()
        {
            return ResultModel.Success("健康检查成功");
        }
    }
}
