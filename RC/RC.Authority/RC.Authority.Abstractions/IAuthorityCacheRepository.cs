namespace RC.Authority.Abstractions
{
    /// <summary>
    /// Authority缓存仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IAuthorityCacheRepository<TDomain> : IRCCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}