namespace RC.Deploy.Repository
{
    /// <summary>
    /// Deploy仓储实现
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    public abstract class DeployRepositoryImpl<TDomain>(DeployDBContext dbContext) : RCRepositoryImpl<TDomain, DeployDBContext>(dbContext), IDeployRepository<TDomain>
        where TDomain : BaseDomain, IDomain, IEntity<Guid>, new()
    {
    }
}