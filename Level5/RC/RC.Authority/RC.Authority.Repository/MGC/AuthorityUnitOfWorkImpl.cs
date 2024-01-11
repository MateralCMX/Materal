namespace RC.Authority.Repository
{
    /// <summary>
    /// Authority工作单元
    /// </summary>
    public partial class AuthorityUnitOfWorkImpl(AuthorityDBContext context, IServiceProvider serviceProvider) : MergeBlockUnitOfWorkImpl<AuthorityDBContext>(context, serviceProvider) { }
}
