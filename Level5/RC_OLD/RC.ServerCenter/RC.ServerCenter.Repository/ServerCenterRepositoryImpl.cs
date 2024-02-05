namespace RC.ServerCenter.Repository
{
    /// <summary>
    /// ServerCenter仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class ServerCenterRepositoryImpl<TDomain>(ServerCenterDBContext dbContext) : RCRepositoryImpl<TDomain, ServerCenterDBContext>(dbContext), IServerCenterRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}