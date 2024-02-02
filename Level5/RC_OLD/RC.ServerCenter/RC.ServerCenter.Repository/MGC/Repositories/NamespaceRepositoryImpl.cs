namespace RC.ServerCenter.Repository.Repositories
{
    /// <summary>
    /// 命名空间仓储
    /// </summary>
    public partial class NamespaceRepositoryImpl(ServerCenterDBContext dbContext) : RCRepositoryImpl<Namespace, Guid, ServerCenterDBContext>(dbContext), INamespaceRepository
    {
    }
}
