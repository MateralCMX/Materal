using Materal.BaseCore.ServiceImpl;
using RC.EnvironmentServer.DataTransmitModel.ConfigurationItem;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.Domain.Repositories;
using RC.EnvironmentServer.Services;
using RC.EnvironmentServer.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.ServiceImpl
{
    /// <summary>
    /// 服务实现
    /// </summary>
    public partial class ConfigurationItemServiceImpl : BaseServiceImpl<AddConfigurationItemModel, EditConfigurationItemModel, QueryConfigurationItemModel, ConfigurationItemDTO, ConfigurationItemListDTO, IConfigurationItemRepository, ConfigurationItem>, IConfigurationItemService
    {
    }
}
