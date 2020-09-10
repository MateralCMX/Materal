using Materal.APP.DataTransmitModel;
using Materal.APP.HttpManage;
using Materal.APP.Services;
using Materal.APP.WebAPICore;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.APP.Server.Controllers
{
    /// <summary>
    /// 服务控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class ServerController : WebAPIControllerBase, IServerManage
    {
        private readonly IServerService _serverService;
        /// <summary>
        /// 
        /// </summary>
        public ServerController(IServerService serverService)
        {
            _serverService = serverService;
        }

        /// <summary>
        /// 获得服务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public Task<ResultModel<List<ServerListDTO>>> GetServerListAsync()
        {
            List<ServerListDTO> result = _serverService.GetServerList();
            return Task.FromResult(ResultModel<List<ServerListDTO>>.Success(result, "获取成功"));
        }
    }
}
