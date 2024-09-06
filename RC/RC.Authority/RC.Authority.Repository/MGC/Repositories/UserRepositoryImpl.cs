/*
 * Generator Code From MateralMergeBlock=>GeneratorRepositoryImplCodeAsync
 */
namespace RC.Authority.Repository.Repositories
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public partial class UserRepositoryImpl(AuthorityDBContext dbContext) : AuthorityRepositoryImpl<User>(dbContext), IUserRepository, IScopedDependency<IUserRepository>
    {
    }
}
