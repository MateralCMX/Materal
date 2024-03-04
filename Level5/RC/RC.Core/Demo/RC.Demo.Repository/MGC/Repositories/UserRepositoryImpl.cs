/*
 * Generator Code From MateralMergeBlock=>GeneratorRepositoryImplCode
 */
namespace RC.Demo.Repository.Repositories
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public partial class UserRepositoryImpl(DemoDBContext dbContext) : DemoRepositoryImpl<User>(dbContext), IUserRepository, IScopedDependencyInjectionService<IUserRepository>
    {
    }
}
