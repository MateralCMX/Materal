using Deploy.Domain;
using Deploy.Domain.Repositories;
using Materal.CacheHelper;

namespace Deploy.SqliteEFRepository.RepositoryImpl
{
    public class DefaultDataRepositoryImpl : DeploySqliteCacheEFRepositoryImpl<DefaultData>, IDefaultDataRepository
    {
        public DefaultDataRepositoryImpl(DeployDBContext dbContext, ICacheManager cacheManager) : base(dbContext, cacheManager)
        {
        }

        public override string GetCacheKey() => "AllDefaultData";
    }
}
