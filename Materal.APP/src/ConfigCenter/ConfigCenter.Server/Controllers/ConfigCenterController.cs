using ConfigCenter.DataTransmitModel.ConfigCenter;
using ConfigCenter.HttpManage;
using ConfigCenter.Services;
using Materal.APP.WebAPICore;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigCenter.Server.Controllers
{
    /// <summary>
    /// 配置中心控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ConfigCenterController : WebAPIControllerBase, IConfigCenterManage
    {
        private readonly IConfigCenterService _configCenterService;

        /// <summary>
        /// 配置中心控制器
        /// </summary>
        public ConfigCenterController(IConfigCenterService configCenterService)
        {
            _configCenterService = configCenterService;
        }

        /// <summary>
        /// 获得环境列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<ResultModel<List<EnvironmentListDTO>>> GetNamespaceList()
        {
            List<EnvironmentListDTO> result = _configCenterService.GetEnvironmentList();
            return Task.FromResult(ResultModel<List<EnvironmentListDTO>>.Success(result, "获取成功"));
        }
    }
}
