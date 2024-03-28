using Microsoft.AspNetCore.Authorization;
using RC.ServerCenter.Abstractions.DTO.Server;

namespace RC.ServerCenter.Abstractions.Controllers
{
    /// <summary>
    /// 服务控制器
    /// </summary>
    public partial interface IServerController : IMergeBlockControllerBase
    {
        /// <summary>
        /// 获得发布程序列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        Task<ResultModel<List<DeployListDTO>>> GetDeployListAsync();
        /// <summary>
        /// 获得环境服务程序列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        Task<ResultModel<List<EnvironmentServerListDTO>>> GetEnvironmentServerListAsync();
        /// <summary>
        /// 获得基础地址
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        ResultModel<string> GetBaseUrl();
    }
}
