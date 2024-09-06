namespace RC.Authority.Repository
{
    /// <summary>
    /// Authority缓存仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class AuthorityCacheRepositoryImpl<TDomain>(AuthorityDBContext dbContext, ICacheHelper cacheHelper) : RCCacheRepositoryImpl<TDomain, AuthorityDBContext>(dbContext, cacheHelper), IAuthorityCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}