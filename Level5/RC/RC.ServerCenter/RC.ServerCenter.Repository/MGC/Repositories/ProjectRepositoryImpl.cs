/*
 * Generator Code From MateralMergeBlock=>GeneratorRepositoryImplCode
 */
namespace RC.ServerCenter.Repository.Repositories
{
    /// <summary>
    /// 项目仓储
    /// </summary>
    public partial class ProjectRepositoryImpl(ServerCenterDBContext dbContext) : ServerCenterRepositoryImpl<Project>(dbContext), IProjectRepository, IScopedDependencyInjectionService<IProjectRepository>
    {
    }
}
