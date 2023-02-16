using Materal.Gateway.Common;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.Controllers
{
    /// <summary>
    /// 网关控制器
    /// </summary>
    public class GatewayController : GatewayWebAPIControllerBase
    {
        /// <summary>
        /// 重新加载配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel ReloadConfigs()
        {
            if (ApplicationConfig.Configuration is IConfigurationRoot configurationRoot)
            {
                configurationRoot.Reload();
                return ResultModel.Success("配置加载完毕");
            }
            return ResultModel.Fail("重新加载配置失败");
        }
        [HttpGet, AllowAnonymous]
        public ResultModel Test()
        {
            return ResultModel.Success("测试");
        }
    }
}
