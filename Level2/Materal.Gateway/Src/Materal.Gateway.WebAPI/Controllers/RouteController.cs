using Materal.Gateway.Common;
using Materal.Gateway.OcelotExtension.ConfigModel;
using Materal.Gateway.OcelotExtension.Services;
using Materal.Gateway.WebAPI.PresentationModel.Route;
using Materal.Utils.Model;
using Microsoft.AspNetCore.Mvc;

namespace Materal.Gateway.WebAPI.Controllers
{
    /// <summary>
    /// 路由控制器
    /// </summary>
    /// <param name="ocelotConfigService"></param>
    public class RouteController(IOcelotConfigService ocelotConfigService) : GatewayControllerBase
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ResultModel<Guid>> AddAsync(AddRouteRequestModel requestModel)
        {
            RouteConfigModel model = requestModel.CopyProperties<RouteConfigModel>();
            model.Index = ocelotConfigService.OcelotConfig.Routes.Count;
            ocelotConfigService.OcelotConfig.Routes.Add(model);
            await ocelotConfigService.SaveAsync();
            return ResultModel<Guid>.Success(model.ID, "添加成功");
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> EditAsync(EditRouteRequestModel requestModel)
        {
            RouteConfigModel routeConfig = ocelotConfigService.OcelotConfig.Routes.FirstOrDefault(m => m.ID == requestModel.ID) ?? throw new GatewayException("路由不存在");
            requestModel.CopyProperties(routeConfig);
            await ocelotConfigService.SaveAsync();
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
            RouteConfigModel routeConfig = ocelotConfigService.OcelotConfig.Routes.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("路由不存在");
            ocelotConfigService.OcelotConfig.Routes.Remove(routeConfig);
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("删除成功");
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<RouteConfigModel> GetInfo(Guid id)
        {
            RouteConfigModel config = ocelotConfigService.OcelotConfig.Routes.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到配置项");
            return ResultModel<RouteConfigModel>.Success(config, "获取成功");
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="questModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<List<RouteConfigModel>> GetList(QueryRouteRequestModel questModel)
        {
            Func<RouteConfigModel, bool> filter = questModel.GetSearchDelegate<RouteConfigModel>();
            List<RouteConfigModel> configs = ocelotConfigService.OcelotConfig.Routes.Where(filter.Invoke).ToList();
            if(questModel.EnableCache is not null)
            {
                configs = questModel.EnableCache.Value
                    ? configs.Where(m => m.FileCacheOptions is not null).ToList()
                    : configs.Where(m => m.FileCacheOptions is null).ToList();
            }
            return ResultModel<List<RouteConfigModel>>.Success(configs, "获取成功");
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
            RouteConfigModel config = ocelotConfigService.OcelotConfig.Routes.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到配置项");
            int upIndex = config.Index - 1;
            if(upIndex < 0) throw new GatewayException("已经是第一个了");
            ocelotConfigService.OcelotConfig.Routes.Remove(config);
            ocelotConfigService.OcelotConfig.Routes.Insert(upIndex, config);
            ocelotConfigService.SetRoutesIndex();
            await ocelotConfigService.SaveAsync();
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
            RouteConfigModel config = ocelotConfigService.OcelotConfig.Routes.FirstOrDefault(m => m.ID == id) ?? throw new GatewayException("未找到配置项");
            int nextIndex = config.Index + 1;
            if(nextIndex >= ocelotConfigService.OcelotConfig.Routes.Count) throw new GatewayException("已经是最后一个了");
            ocelotConfigService.OcelotConfig.Routes.Remove(config);
            ocelotConfigService.OcelotConfig.Routes.Insert(nextIndex, config);
            ocelotConfigService.SetRoutesIndex();
            await ocelotConfigService.SaveAsync();
            return ResultModel.Success("移动成功");
        }
    }
}
