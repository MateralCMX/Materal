using Microsoft.AspNetCore.Authorization;
using RC.Deploy.Abstractions.Enums;

namespace RC.Deploy.Abstractions.HttpClient
{
    /// <summary>
    /// 枚举控制器
    /// </summary>
    [AllowAnonymous]
    public partial class EnumController : MergeBlockControllerBase
    {
        /// <summary>
        /// 获取所有应用程序状态枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<KeyValueModel<ApplicationStatusEnum>>> GetAllApplicationStatusEnum()
        {
            List<KeyValueModel<ApplicationStatusEnum>> result = KeyValueModel<ApplicationStatusEnum>.GetAllCode();
            return ResultModel<List<KeyValueModel<ApplicationStatusEnum>>>.Success(result, "获取成功");
        }
        /// <summary>
        /// 获取所有应用程序类型枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<KeyValueModel<ApplicationTypeEnum>>> GetAllApplicationTypeEnum()
        {
            List<KeyValueModel<ApplicationTypeEnum>> result = KeyValueModel<ApplicationTypeEnum>.GetAllCode();
            return ResultModel<List<KeyValueModel<ApplicationTypeEnum>>>.Success(result, "获取成功");
        }
    }
}
