using Materal.ConfigCenter.ConfigServer.Domain;
using Materal.ConfigCenter.ConfigServer.Domain.Repositories;

namespace Materal.ConfigCenter.ConfigServer.SqliteEFRepository.RepositoryImpl
{
    public class ConfigurationItemRepositoryImpl : ConfigServerSqliteEFRepositoryImpl<ConfigurationItem>, IConfigurationItemRepository
    {
        public ConfigurationItemRepositoryImpl(ConfigServerDBContext dbContext) : base(dbContext)
        {
        }
    }
}
