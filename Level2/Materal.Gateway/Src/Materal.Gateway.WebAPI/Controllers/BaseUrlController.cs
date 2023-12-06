using Materal.Gateway.Service;
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
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SetConfigAsync(string? baseUrl)
        {
            await ocelotConfigService.SetBaseUrlAsync(baseUrl);
            return ResultModel.Success("设置成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<string?> GetConfig()
        {
            string? reusult = ocelotConfigService.GetBaseUrl();
            return ResultModel<string?>.Success(reusult, "获取成功");
        }
    }
}
