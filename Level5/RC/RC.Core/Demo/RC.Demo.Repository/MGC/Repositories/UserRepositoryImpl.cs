namespace RC.Demo.Repository.Repositories
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public partial class UserRepositoryImpl(DemoDBContext dbContext) : RCRepositoryImpl<User, Guid, DemoDBContext>(dbContext), IUserRepository
    {
    }
}
