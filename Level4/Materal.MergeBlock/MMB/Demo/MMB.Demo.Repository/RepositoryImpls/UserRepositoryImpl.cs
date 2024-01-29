namespace MMB.Demo.Repository.RepositoryImpls
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepositoryImpl(DemoDBContext context) : DemoRepositoryImpl<User>(context), IUserRepository
    {
    }
}
