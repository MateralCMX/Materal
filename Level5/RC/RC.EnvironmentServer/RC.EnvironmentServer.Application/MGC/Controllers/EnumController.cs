using Microsoft.AspNetCore.Authorization;
using RC.EnvironmentServer.Abstractions.Enums;

namespace RC.EnvironmentServer.Abstractions.HttpClient
{
    /// <summary>
    /// 枚举控制器
    /// </summary>
    [AllowAnonymous]
    public partial class EnumController : MergeBlockControllerBase
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
