using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.Abstractions.Services
{
    /// <summary>
    /// 配置项服务
    /// </summary>
    public partial interface IConfigurationItemService : IBaseService<AddConfigurationItemModel, EditConfigurationItemModel, QueryConfigurationItemModel, ConfigurationItemDTO, ConfigurationItemListDTO>
    {
    }
}
