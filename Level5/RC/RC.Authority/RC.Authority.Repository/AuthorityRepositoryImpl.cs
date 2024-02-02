namespace RC.Authority.Repository
{
    /// <summary>
    /// Authority仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class AuthorityRepositoryImpl<TDomain>(AuthorityDBContext dbContext) : RCRepositoryImpl<TDomain, AuthorityDBContext>(dbContext), IAuthorityRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}