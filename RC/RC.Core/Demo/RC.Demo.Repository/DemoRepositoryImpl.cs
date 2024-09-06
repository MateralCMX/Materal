namespace RC.Demo.Repository
{
    /// <summary>
    /// Demo仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class DemoRepositoryImpl<TDomain>(DemoDBContext dbContext) : RCRepositoryImpl<TDomain, DemoDBContext>(dbContext), IDemoRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}