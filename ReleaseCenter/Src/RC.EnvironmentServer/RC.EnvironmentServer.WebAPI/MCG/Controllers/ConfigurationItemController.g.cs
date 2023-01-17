using Materal.BaseCore.WebAPI.Controllers;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.PresentationModel.ConfigurationItem;
using RC.EnvironmentServer.Services;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.WebAPI.Controllers
{
    /// <summary>
    /// 配置项控制器
    /// </summary>
    public partial class ConfigurationItemController : MateralCoreWebAPIServiceControllerBase<AddConfigurationItemRequestModel, EditConfigurationItemRequestModel, QueryConfigurationItemRequestModel, AddConfigurationItemModel, EditConfigurationItemModel, QueryConfigurationItemModel, ConfigurationItemDTO, ConfigurationItemListDTO, IConfigurationItemService>
    {
    }
}
