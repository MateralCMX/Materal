namespace RC.ServerCenter.Abstractions
{
    /// <summary>
    /// ServerCenter仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IServerCenterRepository<TDomain> : IRCRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}