using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RC.Core.Common;

namespace RC.Core.WebAPI.Controllers
{
    /// <summary>
    /// 新明解中心控制器
    /// </summary>
    public class RCCoreController : RCWebAPIControllerBase
    {
        /// <summary>
        /// 重新加载配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel ReloadConfigs()
        {
            if(RCConfig.Configuration is IConfigurationRoot configurationRoot)
            {
                configurationRoot.Reload();
                return ResultModel.Success("配置加载完毕");
            }
            return ResultModel.Fail("重新加载配置失败");
        }
    }
}
