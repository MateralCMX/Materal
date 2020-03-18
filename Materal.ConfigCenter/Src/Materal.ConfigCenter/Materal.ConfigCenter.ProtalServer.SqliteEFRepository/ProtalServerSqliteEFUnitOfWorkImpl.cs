using Materal.TTA.SqliteRepository;

namespace Materal.ConfigCenter.ProtalServer.SqliteEFRepository
{
    public class ProtalServerSqliteEFUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<ProtalServerDBContext>, IProtalServerUnitOfWork
    {
        public ProtalServerSqliteEFUnitOfWorkImpl(ProtalServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
