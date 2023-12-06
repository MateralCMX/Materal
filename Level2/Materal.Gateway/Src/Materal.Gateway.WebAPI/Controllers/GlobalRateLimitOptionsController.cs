using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.Service;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// 全局限流配置控制器
    /// </summary>
    public class GlobalRateLimitOptionsController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SetConfigAsync(GlobalRateLimitOptionsModel? model)
        {
            await ocelotConfigService.SetGlobalRateLimitOptionsAsync(model);
            return ResultModel.Success("设置成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<GlobalRateLimitOptionsModel?> GetConfig()
        {
            GlobalRateLimitOptionsModel? result = ocelotConfigService.GetGlobalRateLimitOptions();
            return ResultModel<GlobalRateLimitOptionsModel?>.Success(result, "获取成功");
        }
    }
}
