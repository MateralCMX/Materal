using MMB.Demo.Abstractions;

namespace MMB.Demo.Repository
{
    /// <summary>
    /// Demo仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class DemoRepositoryImpl<TDomain>(DemoDBContext dbContext) : MMBRepositoryImpl<TDomain, DemoDBContext>(dbContext), IDemoRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}
