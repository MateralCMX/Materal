/*
 * Generator Code From MateralMergeBlock=>GeneratorIControllerCodeAsync
 */
using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.RequestModel.ConfigurationItem;

namespace RC.EnvironmentServer.Abstractions.Controllers
{
    /// <summary>
    /// 配置项控制器
    /// </summary>
    public partial interface IConfigurationItemController : IMergeBlockController<AddConfigurationItemRequestModel, EditConfigurationItemRequestModel, QueryConfigurationItemRequestModel, ConfigurationItemDTO, ConfigurationItemListDTO>
    {
    }
}
