namespace MMB.Demo.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public partial class UserRepositoryImpl(DemoDBContext dbContext) : MMBRepositoryImpl<User, Guid, DemoDBContext>(dbContext), IUserRepository
    {
    }
}