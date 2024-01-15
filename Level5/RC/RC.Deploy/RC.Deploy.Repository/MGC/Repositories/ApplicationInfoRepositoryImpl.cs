namespace RC.Deploy.Repository.Repositories
{
    /// <summary>
    /// 应用程序信息仓储
    /// </summary>
    public partial class ApplicationInfoRepositoryImpl(DeployDBContext dbContext) : RCRepositoryImpl<ApplicationInfo, Guid, DeployDBContext>(dbContext), IApplicationInfoRepository
    {
    }
}
