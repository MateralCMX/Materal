namespace RC.Authority.Repository
{
    /// <summary>
    /// Authority工作单元实现
    /// </summary>
    /// <param name="context"></param>
    /// <param name="serviceProvider"></param>
    public class AuthorityUnitOfWorkImpl(AuthorityDBContext context, IServiceProvider serviceProvider) : RCUnitOfWorkImpl<AuthorityDBContext>(context, serviceProvider), IAuthorityUnitOfWork, IScopedDependency<IAuthorityUnitOfWork>
    {
    }
}