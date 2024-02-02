namespace RC.ServerCenter.Repository.Repositories
{
    /// <summary>
    /// 项目仓储
    /// </summary>
    public partial class ProjectRepositoryImpl(ServerCenterDBContext dbContext) : RCRepositoryImpl<Project, Guid, ServerCenterDBContext>(dbContext), IProjectRepository
    {
    }
}
