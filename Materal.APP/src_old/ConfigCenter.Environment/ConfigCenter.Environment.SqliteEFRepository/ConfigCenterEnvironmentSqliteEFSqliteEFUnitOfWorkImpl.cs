using Materal.TTA.SqliteRepository;

namespace ConfigCenter.Environment.SqliteEFRepository
{
    public class ConfigCenterEnvironmentSqliteEFUnitOfWorkImpl : SqliteEFUnitOfWorkImpl<ConfigCenterEnvironmentDBContext>, IConfigCenterEnvironmentSqliteEFUnitOfWork
    {
        public ConfigCenterEnvironmentSqliteEFUnitOfWorkImpl(ConfigCenterEnvironmentDBContext dbContext) : base(dbContext)
        {
        }
    }
}
