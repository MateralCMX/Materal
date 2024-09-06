namespace RC.ServerCenter.Abstractions
{
    /// <summary>
    /// ServerCenter缓存仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IServerCenterCacheRepository<TDomain> : IRCCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}