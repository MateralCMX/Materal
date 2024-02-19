using Materal.Gateway.Service.Models.Tools;

namespace Materal.Gateway.Controllers
{
    /// <summary>
    /// 枚举控制器
    /// </summary>
    public class EnumController : GatewayControllerBase
    {
        /// <summary>
        /// 获取所有SyncMode枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<KeyValueModel<SyncModeEnum>>> GetAllSyncMode() => ResultModel<List<KeyValueModel<SyncModeEnum>>>.Success(KeyValueModel<SyncModeEnum>.GetAllCode(), "获取成功");
    }
}
