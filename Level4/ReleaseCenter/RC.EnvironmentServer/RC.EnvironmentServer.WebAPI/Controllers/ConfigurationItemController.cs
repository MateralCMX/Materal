using Materal.Utils.Model;
using Microsoft.AspNetCore.Authorization;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;

namespace RC.EnvironmentServer.WebAPI.Controllers
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
        {
            return base.GetListAsync(requestModel);
        }
    }
}
