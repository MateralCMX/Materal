namespace RC.Core.Abstractions
{
    /// <summary>
    /// RC仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IRCRepository<TDomain> : IEFRepository<TDomain, Guid>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}