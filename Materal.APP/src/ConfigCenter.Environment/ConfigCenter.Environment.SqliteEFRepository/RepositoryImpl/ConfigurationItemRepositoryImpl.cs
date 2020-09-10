using ConfigCenter.Environment.Domain;
using ConfigCenter.Environment.Domain.Repositories;

namespace ConfigCenter.Environment.SqliteEFRepository.RepositoryImpl
{
    public class ConfigurationItemRepositoryImpl : ConfigCenterEnvironmentSqliteEFRepositoryImpl<ConfigurationItem>, IConfigurationItemRepository
    {
        public ConfigurationItemRepositoryImpl(ConfigCenterEnvironmentDBContext dbContext) : base(dbContext)
        {
        }
    }
}
