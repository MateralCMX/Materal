using Materal.BaseCore.Services;
using Materal.BaseCore.Services.Models;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.Services
{
    /// <summary>
    /// 配置项服务
    /// </summary>
    public partial interface IConfigurationItemService : IBaseService<AddConfigurationItemModel, EditConfigurationItemModel, QueryConfigurationItemModel, ConfigurationItemDTO, ConfigurationItemListDTO>
    {
    }
}
