using Materal.TTA.SqliteRepository;

namespace Materal.ConDep.Center.SqliteEFRepository
{
    public class CenterSqliteEFUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<CenterDBContext>, ICenterSqliteEFUnitOfWork
    {
        public CenterSqliteEFUnitOfWorkImpl(CenterDBContext dbContext) : base(dbContext)
        {
        }
    }
}
