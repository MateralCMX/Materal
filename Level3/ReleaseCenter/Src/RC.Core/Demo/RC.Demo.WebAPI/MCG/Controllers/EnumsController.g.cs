using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RC.Demo.Enums;

namespace RC.Demo.WebAPI.Controllers
{
    /// <summary>
    /// 枚举控制器
    /// </summary>
    [AllowAnonymous, NoAutoDI]
    public partial class EnumsController : MateralCoreWebAPIControllerBase
    {
        /// <summary>
        /// 获取所有性别枚举
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<KeyValueModel<SexEnum>>> GetAllSexEnum()
        {
            List<KeyValueModel<SexEnum>> result = KeyValueModel<SexEnum>.GetAllCode();
            return ResultModel<List<KeyValueModel<SexEnum>>>.Success(result, "获取成功");
        }
    }
}
