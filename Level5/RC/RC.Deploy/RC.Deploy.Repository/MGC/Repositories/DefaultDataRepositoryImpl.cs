namespace RC.Deploy.Repository.Repositories
{
    /// <summary>
    /// 默认数据仓储
    /// </summary>
    public partial class DefaultDataRepositoryImpl(DeployDBContext dbContext) : DeployRepositoryImpl<DefaultData>(dbContext), IDefaultDataRepository, IScopedDependencyInjectionService<IDefaultDataRepository>
    {
    }
}
