namespace RC.EnvironmentServer.Abstractions
{
    /// <summary>
    /// EnvironmentServer仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IEnvironmentServerRepository<TDomain> : IRCRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}