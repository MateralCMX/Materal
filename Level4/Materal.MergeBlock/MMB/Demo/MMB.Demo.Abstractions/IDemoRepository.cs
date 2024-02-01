namespace MMB.Demo.Abstractions
{
    /// <summary>
    /// Demo仓储接口
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public interface IDemoRepository<TDomain> : IMMBRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}
