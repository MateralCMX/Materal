namespace RC.Core.Repository
{
    /// <summary>
    /// RC仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class RCRepositoryImpl<TDomain, TDBContext>(TDBContext dbContext) : SqliteEFRepositoryImpl<TDomain, Guid, TDBContext>(dbContext), IRCRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
        where TDBContext : DbContext
    {
    }
}