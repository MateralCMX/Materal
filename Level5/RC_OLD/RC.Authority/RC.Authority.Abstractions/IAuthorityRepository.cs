namespace RC.Authority.Abstractions
{
    /// <summary>
    /// Authority仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IAuthorityRepository<TDomain> : IRCRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}