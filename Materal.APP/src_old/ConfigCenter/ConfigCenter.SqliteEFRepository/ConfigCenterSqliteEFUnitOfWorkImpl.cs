using Materal.TTA.SqliteRepository;

namespace ConfigCenter.SqliteEFRepository
{
    public class ConfigCenterSqliteEFUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<ConfigCenterDBContext>, IConfigCenterSqliteEFUnitOfWork
    {
        public ConfigCenterSqliteEFUnitOfWorkImpl(ConfigCenterDBContext dbContext) : base(dbContext)
        {
        }
    }
}
