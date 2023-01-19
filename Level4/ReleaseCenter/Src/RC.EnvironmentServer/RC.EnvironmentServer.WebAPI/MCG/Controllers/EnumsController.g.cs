using Materal.BaseCore.WebAPI.Controllers;
using Materal.Model;
using Materal.EnumHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RC.EnvironmentServer.Enums;

namespace RC.EnvironmentServer.WebAPI.Controllers
{
    /// <summary>
    /// 枚举控制器
    /// </summary>
    [AllowAnonymous]
    public partial class EnumsController : MateralCoreWebAPIControllerBase
    {
        /// <summary>
        /// 获取所有同步模式枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<KeyValueModel<SyncModeEnum>>> GetAllSyncModeEnum()
        {
            List<KeyValueModel<SyncModeEnum>> result = KeyValueModel<SyncModeEnum>.GetAllCode();
            return ResultModel<List<KeyValueModel<SyncModeEnum>>>.Success(result, "获取成功");
        }
    }
}
