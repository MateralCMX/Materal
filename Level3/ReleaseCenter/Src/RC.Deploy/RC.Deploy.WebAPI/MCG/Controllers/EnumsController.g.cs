using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RC.Deploy.Enums;

namespace RC.Deploy.WebAPI.Controllers
{
    /// <summary>
    /// 枚举控制器
    /// </summary>
    [AllowAnonymous, NoAutoDI]
    public partial class EnumsController : MateralCoreWebAPIControllerBase
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
