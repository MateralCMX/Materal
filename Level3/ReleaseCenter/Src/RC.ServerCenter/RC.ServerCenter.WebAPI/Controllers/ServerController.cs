using AutoMapper;
using Materal.BaseCore.WebAPI;
using Materal.BaseCore.WebAPI.Controllers;
using Materal.BaseCore.WebAPI.Models;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using RC.ServerCenter.DataTransmitModel.Server;

namespace RC.ServerCenter.WebAPI.Controllers
{
    /// <summary>
    /// 服务控制器
    /// </summary>
    public class ServerController : MateralCoreWebAPIControllerBase
    {
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ServerController(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// 获得发布程序列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<DeployListDTO>>> GetDeployListAsync()
        {
            List<ConsulServiceModel> consulServices = await ConsulManager.GetServicesAsync(m => m.Tags != null && m.Tags.Length > 0 && m.Tags.Contains("RC.Deploy"));
            List<DeployListDTO> result = _mapper.Map<List<DeployListDTO>>(consulServices);
            return ResultModel<List<DeployListDTO>>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得环境服务程序列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<EnvironmentServerListDTO>>> GetEnvironmentServerListAsync()
        {
            List<ConsulServiceModel> consulServices = await ConsulManager.GetServicesAsync(m => m.Tags != null && m.Tags.Length > 0 && m.Tags.Contains("RC.EnvironmentServer"));
            List<EnvironmentServerListDTO> result = _mapper.Map<List<EnvironmentServerListDTO>>(consulServices);
            return ResultModel<List<EnvironmentServerListDTO>>.Success(result, "查询成功");
        }
    }
}
