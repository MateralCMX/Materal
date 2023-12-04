using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.OcelotExtension.Services;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// 服务发现提供者控制器
    /// </summary>
    public class ServiceDiscoveryProviderController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SetConfigAsync(ServiceDiscoveryProviderModel? model)
        {
            ocelotConfigService.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider = model;
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("设置成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<ServiceDiscoveryProviderModel?> GetConfig()
        {
            ServiceDiscoveryProviderModel? result = ocelotConfigService.OcelotConfig.GlobalConfiguration.ServiceDiscoveryProvider;
            return ResultModel<ServiceDiscoveryProviderModel?>.Success(result, "获取成功");
        }
    }
}
