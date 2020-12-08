using ConfigCenter.Environment.Domain;
using ConfigCenter.Environment.Domain.Repositories;
using Materal.CacheHelper;

namespace ConfigCenter.Environment.SqliteEFRepository.RepositoryImpl
{
    public class ConfigurationItemRepositoryImpl : ConfigCenterEnvironmentCacheSqliteEFRepositoryImpl<ConfigurationItem>, IConfigurationItemRepository
    {
        public ConfigurationItemRepositoryImpl(ConfigCenterEnvironmentDBContext dbContext, ICacheManager cacheManager) : base(dbContext, cacheManager)
        {
        }

        public override string GetCacheKey() => "AllConfigurationItem";
    }
}
