/*
 * Generator Code From MateralMergeBlock=>GeneratorRepositoryImplCodeAsync
 */
namespace RC.Deploy.Repository.Repositories
{
    /// <summary>
    /// 应用程序信息仓储
    /// </summary>
    public partial class ApplicationInfoRepositoryImpl(DeployDBContext dbContext) : DeployRepositoryImpl<ApplicationInfo>(dbContext), IApplicationInfoRepository, IScopedDependency<IApplicationInfoRepository>
    {
    }
}
