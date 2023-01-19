using Materal.Gateway.WebAPI.Services;
using Materal.Gateway.WebAPI.Services.Models;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.Gateway.WebAPI.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class RouteController : BaseController
    {
        private readonly IRouteServices _routeServices;

        public RouteController(IRouteServices routeServices)
        {
            _routeServices = routeServices;
        }

        [HttpPut]
        public async Task<ResultModel> AddAsync(RouteModel model)
        {
            await _routeServices.AddAsync(model);
            return ResultModel.Success("添加成功");
        }
        [HttpPut]
        public async Task<ResultModel> EditAsync(RouteModel model)
        {
            await _routeServices.EditAsync(model);
            return ResultModel.Success("修改成功");
        }
        [HttpDelete]
        public async Task<ResultModel> Delete(string serviceName)
        {
            await _routeServices.DeleteAsync(serviceName);
            return ResultModel.Success("删除成功");
        }
        [HttpPost]
        public async Task<ResultModel<List<RouteListModel>>> GetListAsync(QueryRouteFilterModel filterModel)
        {
            List<RouteListModel> data = await _routeServices.GetListAsync(filterModel);
            return ResultModel<List<RouteListModel>>.Success(data, "获取成功");
        }
        [HttpGet]
        public async Task<ResultModel<RouteModel>> GetAsync(string serviceName)
        {
            RouteModel data = await _routeServices.GetAsync(serviceName);
            return ResultModel<RouteModel>.Success(data, "获取成功");
        }
    }
}
