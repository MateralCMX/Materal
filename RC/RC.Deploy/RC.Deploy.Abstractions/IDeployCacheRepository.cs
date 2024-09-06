namespace RC.Deploy.Abstractions
{
    /// <summary>
    /// Deploy缓存仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IDeployCacheRepository<TDomain> : IRCCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}