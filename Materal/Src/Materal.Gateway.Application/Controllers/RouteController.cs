using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.Service;
using Materal.Gateway.Service.Models.Route;

namespace Materal.Gateway.Application.Controllers
{
    /// <summary>
    /// 路由控制器
    /// </summary>
    public class RouteController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddAsync(AddRouteModel model)
        {
            Guid result = await ocelotConfigService.AddRouteAsync(model);
            return ResultModel<Guid>.Success(result, "添加成功");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditAsync(EditRouteModel model)
        {
            await ocelotConfigService.EditRouteAsync(model);
            return ResultModel.Success("修改成功");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ResultModel> DeleteAsync(Guid id)
        {
            await ocelotConfigService.DeleteRouteAsync(id);
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<RouteConfigModel> GetInfo(Guid id)
        {
            RouteConfigModel config = ocelotConfigService.GetRouteInfo(id);
            return ResultModel<RouteConfigModel>.Success(config, "获取成功");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<List<RouteConfigModel>> GetList(QueryRouteModel model)
        {
            List<RouteConfigModel> result = ocelotConfigService.GetRouteList(model);
            return ResultModel<List<RouteConfigModel>>.Success(result, "获取成功");
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        [HttpPost]
        public async Task<ResultModel> MoveUpAsync(Guid id)
        {
            await ocelotConfigService.MoveUpRouteAsync(id);
            return ResultModel.Success("移动成功");
        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="GatewayException"></exception>
        [HttpPost]
        public async Task<ResultModel> MoveNextAsync(Guid id)
        {
            await ocelotConfigService.MoveNextRouteAsync(id);
            return ResultModel.Success("移动成功");
        }
    }
}
