using Materal.Gateway.OcelotExtension.Services;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// BaseUrl控制器
    /// </summary>
    public class BaseUrlController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SetConfigAsync(string? model)
        {
            ocelotConfigService.OcelotConfig.GlobalConfiguration.BaseUrl = model;
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("设置成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<string?> GetConfig()
        {
            string? result = ocelotConfigService.OcelotConfig.GlobalConfiguration.BaseUrl;
            return ResultModel<string?>.Success(result, "获取成功");
        }
    }
}
