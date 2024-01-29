namespace MMB.Core.Repository
{
    /// <summary>
    /// MMB仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IMMBRepository<TDomain> : IEFRepository<TDomain, Guid>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}
