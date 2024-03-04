/*
 * Generator Code From MateralMergeBlock=>GeneratorControllerAccessor
 */
using Microsoft.AspNetCore.Authorization;
using RC.ServerCenter.Abstractions.DTO.Server;

namespace RC.ServerCenter.Abstractions.ControllerAccessors
{
    /// <summary>
    /// 服务控制器访问器
    /// </summary>
    public partial class ServerControllerAccessor(IServiceProvider serviceProvider) : BaseControllerAccessor(serviceProvider), IServerController
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public override string ProjectName => "RC";
        /// <summary>
        /// 模块名称
        /// </summary>
        public override string ModuleName => "ServerCenter";
        /// <summary>
        /// 获得发布程序列表
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel<List<DeployListDTO>>> GetDeployListAsync()
            => await HttpHelper.SendAsync<IServerController, ResultModel<List<DeployListDTO>>>(ProjectName, ModuleName, nameof(GetDeployListAsync), []);
        /// <summary>
        /// 获得环境服务程序列表
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel<List<EnvironmentServerListDTO>>> GetEnvironmentServerListAsync()
            => await HttpHelper.SendAsync<IServerController, ResultModel<List<EnvironmentServerListDTO>>>(ProjectName, ModuleName, nameof(GetEnvironmentServerListAsync), []);
        /// <summary>
        /// 获得基础地址
        /// </summary>
        /// <returns></returns>
        public ResultModel<string> GetBaseUrl()
            => HttpHelper.SendAsync<IServerController, ResultModel<string>>(ProjectName, ModuleName, nameof(GetBaseUrl), []).Result;
    }
}
