namespace RC.EnvironmentServer.Repository.Repositories
{
    /// <summary>
    /// 配置项仓储
    /// </summary>
    public partial class ConfigurationItemRepositoryImpl(EnvironmentServerDBContext dbContext) : RCRepositoryImpl<ConfigurationItem, Guid, EnvironmentServerDBContext>(dbContext), IConfigurationItemRepository
    {
    }
}
