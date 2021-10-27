using Materal.TTA.SqliteRepository;

namespace Authority.SqliteEFRepository
{
    public class AuthoritySqliteEFUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<AuthorityDBContext>, IAuthoritySqliteEFUnitOfWork
    {
        public AuthoritySqliteEFUnitOfWorkImpl(AuthorityDBContext dbContext) : base(dbContext)
        {
        }
    }
}
