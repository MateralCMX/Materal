/*
 * Generator Code From MateralMergeBlock=>GeneratorServiceImplsCode
 */
using RC.EnvironmentServer.Abstractions.DTO.ConfigurationItem;
using RC.EnvironmentServer.Abstractions.Services.Models.ConfigurationItem;

namespace RC.EnvironmentServer.Application.Services
{
    /// <summary>
    /// 配置项服务
    /// </summary>
    public partial class ConfigurationItemServiceImpl : BaseServiceImpl<AddConfigurationItemModel, EditConfigurationItemModel, QueryConfigurationItemModel, ConfigurationItemDTO, ConfigurationItemListDTO, IConfigurationItemRepository, ConfigurationItem, IEnvironmentServerUnitOfWork>, IConfigurationItemService, IScopedDependencyInjectionService<IConfigurationItemService>
    {
    }
}
