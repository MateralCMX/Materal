using Materal.Gateway.Service;
using Materal.Gateway.Service.Models.Tools;

namespace Materal.Gateway.Application.Controllers
{
    /// <summary>
    /// 同步控制器
    /// </summary>
    public class ToolsController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 从Consul获取Swagger
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> GetSwaggerFromConsulAsync(GetSwaggerFromConsulModel model)
        {
            await ocelotConfigService.GetSwaggerFromConsulAsync(model);
            return ResultModel.Success("获取成功");
        }
        /// <summary>
        /// 从Consul获取路由
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> GetRouteFromConsulAsync(GetRouteFromConsulModel model)
        {
            await ocelotConfigService.GetRouteFromConsulAsync(model);
            return ResultModel.Success("获取成功");
        }
    }
}
