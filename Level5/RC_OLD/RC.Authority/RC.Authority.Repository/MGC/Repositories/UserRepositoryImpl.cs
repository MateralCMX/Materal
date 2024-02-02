namespace RC.Authority.Repository.Repositories
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public partial class UserRepositoryImpl(AuthorityDBContext dbContext) : RCRepositoryImpl<User, Guid, AuthorityDBContext>(dbContext), IUserRepository
    {
    }
}
