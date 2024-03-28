using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;

namespace RC.EnvironmentServer.Abstractions.Controllers
{
    /// <summary>
    /// 控制器
    /// </summary>
    public partial interface IConfigurationItemController : IMergeBlockControllerBase
    {
        /// <summary>
        /// 同步配置
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        Task<ResultModel> SyncConfigAsync(SyncConfigRequestModel requestModel);
    }
}
