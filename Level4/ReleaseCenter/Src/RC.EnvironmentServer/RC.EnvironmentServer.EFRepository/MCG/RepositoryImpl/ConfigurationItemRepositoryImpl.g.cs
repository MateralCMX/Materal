using RC.Core.EFRepository;
using RC.EnvironmentServer.Domain;
using RC.EnvironmentServer.Domain.Repositories;
using Materal.CacheHelper;

namespace RC.EnvironmentServer.EFRepository.RepositoryImpl
{
    /// <summary>
    /// 配置项仓储实现
    /// </summary>
    public partial class ConfigurationItemRepositoryImpl: RCCacheRepositoryImpl<ConfigurationItem, Guid>, IConfigurationItemRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ConfigurationItemRepositoryImpl(EnvironmentServerDBContext dbContext, ICacheManager cacheManager) : base(dbContext, cacheManager) { }
        /// <summary>
        /// 获得所有缓存名称
        /// </summary>
        protected override string GetAllCacheName() => "AllConfigurationItem";
    }
}
