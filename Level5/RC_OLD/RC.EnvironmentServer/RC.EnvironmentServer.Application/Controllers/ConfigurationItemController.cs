using Microsoft.AspNetCore.Authorization;
using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;

namespace RC.EnvironmentServer.Application.Controllers
{
    /// <summary>
    /// 配置项控制器
    /// </summary>
    public partial class ConfigurationItemController
    {
        /// <summary>
        /// 获得列表信息
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public override Task<PageResultModel<ConfigurationItemListDTO>> GetListAsync(QueryConfigurationItemRequestModel requestModel) 
            => base.GetListAsync(requestModel);
    }
}
