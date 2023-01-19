using Materal.TTA.Common;
namespace Authority.EFRepository
{
    /// <summary>
    /// Authority工作单元
    /// </summary>
    public class AuthorityUnitOfWorkImpl : EFUnitOfWorkImpl<AuthorityDbContext>, IAuthorityUnitOfWork
    {
        public AuthorityUnitOfWorkImpl(AuthorityDbContext context) : base(context)
        {
        }
    }
}
