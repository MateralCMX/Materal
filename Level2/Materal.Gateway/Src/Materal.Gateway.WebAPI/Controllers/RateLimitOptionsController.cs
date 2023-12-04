using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.OcelotExtension.Services;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// 限流配置控制器
    /// </summary>
    public class RateLimitOptionsController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SetConfigAsync(GlobalRateLimitOptionsModel? model)
        {
            ocelotConfigService.OcelotConfig.GlobalConfiguration.RateLimitOptions = model;
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("设置成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<GlobalRateLimitOptionsModel?> GetConfig()
        {
            GlobalRateLimitOptionsModel? result = ocelotConfigService.OcelotConfig.GlobalConfiguration.RateLimitOptions;
            return ResultModel<GlobalRateLimitOptionsModel?>.Success(result, "获取成功");
        }
    }
}
