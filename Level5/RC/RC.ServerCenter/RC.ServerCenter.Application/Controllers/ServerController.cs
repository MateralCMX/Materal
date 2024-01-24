﻿using Materal.Utils.Consul;
using Materal.Utils.Consul.Models;
using RC.ServerCenter.Abstractions.DTO.Server;

namespace RC.ServerCenter.WebAPI.Controllers
{
    /// <summary>
    /// 服务控制器
    /// </summary>
    public partial class ServerController(IMapper mapper, IConsulService consulService) : MergeBlockControllerBase
    {
        /// <summary>
        /// 获得发布程序列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<DeployListDTO>>> GetDeployListAsync()
        {
            List<ConsulServiceModel> consulServices = await consulService.GetServiceListAsync(m => m.Tags != null && m.Tags.Length > 0 && m.Tags.Contains("RC.Deploy"));
            List<DeployListDTO> result = mapper.Map<List<DeployListDTO>>(consulServices);
            return ResultModel<List<DeployListDTO>>.Success(result, "查询成功");
        }
        /// <summary>
        /// 获得环境服务程序列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<List<EnvironmentServerListDTO>>> GetEnvironmentServerListAsync()
        {
            List<ConsulServiceModel> consulServices = await consulService.GetServiceListAsync(m => m.Tags != null && m.Tags.Length > 0 && m.Tags.Contains("RC.EnvironmentServer"));
            List<EnvironmentServerListDTO> result = mapper.Map<List<EnvironmentServerListDTO>>(consulServices);
            return ResultModel<List<EnvironmentServerListDTO>>.Success(result, "查询成功");
        }
    }
}