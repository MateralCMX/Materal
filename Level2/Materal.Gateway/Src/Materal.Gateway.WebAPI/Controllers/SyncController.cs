using Materal.Gateway.Service;
using Materal.Gateway.Service.Models.Sync;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// 同步控制器
    /// </summary>
    public class SyncController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 同步Swagger
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SyncSwaggerAsync(SyncSwaggerModel model)
        {
            await ocelotConfigService.SyncSwaggerAsync(model);
            return ResultModel.Success("同步成功");
        }
        /// <summary>
        /// 同步路由
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> SyncRouteAsync(SyncRouteModel model)
        {
            await ocelotConfigService.SyncRouteAsync(model);
            return ResultModel.Success("同步成功");
        }
    }
}
