namespace RC.Core.Abstractions
{
    /// <summary>
    /// RC缓存仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IRCCacheRepository<TDomain> : ICacheEFRepository<TDomain, Guid>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}