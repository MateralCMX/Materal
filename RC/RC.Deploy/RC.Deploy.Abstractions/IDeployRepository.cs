namespace RC.Deploy.Abstractions
{
    /// <summary>
    /// Deploy仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IDeployRepository<TDomain> : IRCRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}