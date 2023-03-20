using Materal.TTA.EFRepository;

namespace RC.EnvironmentServer.Domain.Repositories
{
    /// <summary>
    /// 配置项仓储接口
    /// </summary>
    public partial interface IConfigurationItemRepository : IEFRepository<ConfigurationItem, Guid>
    {
    }
}
