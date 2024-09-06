namespace RC.Deploy.Repository
{
    /// <summary>
    /// Deploy缓存仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class DeployCacheRepositoryImpl<TDomain>(DeployDBContext dbContext, ICacheHelper cacheHelper) : RCCacheRepositoryImpl<TDomain, DeployDBContext>(dbContext, cacheHelper), IDeployCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}