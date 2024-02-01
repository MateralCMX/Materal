namespace MMB.Core.Abstractions
{
    /// <summary>
    /// MMB缓存仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IMMBCacheRepository<TDomain> : ICacheEFRepository<TDomain, Guid>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}
