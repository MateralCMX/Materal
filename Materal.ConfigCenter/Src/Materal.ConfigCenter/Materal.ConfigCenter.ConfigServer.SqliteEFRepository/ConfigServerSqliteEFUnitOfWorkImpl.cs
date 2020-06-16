using Materal.TTA.SqliteRepository;

namespace Materal.ConfigCenter.ConfigServer.SqliteEFRepository
{
    public class ConfigServerSqliteEFUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<ConfigServerDBContext>, IConfigServerUnitOfWork
    {
        public ConfigServerSqliteEFUnitOfWorkImpl(ConfigServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
