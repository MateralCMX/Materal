namespace RC.EnvironmentServer.Abstractions
{
    /// <summary>
    /// EnvironmentServer缓存仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IEnvironmentServerCacheRepository<TDomain> : IRCCacheRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}