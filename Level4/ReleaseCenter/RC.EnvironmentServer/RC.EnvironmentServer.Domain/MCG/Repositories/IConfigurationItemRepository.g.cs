using Materal.TTA.EFRepository;
using RC.Core.Domain.Repositories;
using RC.EnvironmentServer.Enums;

namespace RC.EnvironmentServer.Domain.Repositories
{
    /// <summary>
    /// 配置项仓储接口
    /// </summary>
    public partial interface IConfigurationItemRepository : IRCRepository<ConfigurationItem, Guid>
    {
    }
}
