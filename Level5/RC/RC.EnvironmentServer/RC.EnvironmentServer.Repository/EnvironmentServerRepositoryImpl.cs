namespace RC.EnvironmentServer.Repository
{
    /// <summary>
    /// EnvironmentServer仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class EnvironmentServerRepositoryImpl<TDomain>(EnvironmentServerDBContext dbContext) : RCRepositoryImpl<TDomain, EnvironmentServerDBContext>(dbContext), IEnvironmentServerRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}