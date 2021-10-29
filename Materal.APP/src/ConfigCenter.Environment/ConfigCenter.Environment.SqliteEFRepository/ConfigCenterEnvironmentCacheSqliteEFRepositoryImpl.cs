using Materal.CacheHelper;
using Materal.TTA.Common;
using Materal.TTA.EFRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.DateTimeHelper;

namespace ConfigCenter.Environment.SqliteEFRepository
{
    public abstract class ConfigCenterEnvironmentCacheSqliteEFRepositoryImpl<T> : ConfigCenterEnvironmentSqliteEFRepositoryImpl<T>, ICacheEFRepository<T, Guid> where T : class, IEntity<Guid>, new()
    {
        protected readonly ICacheManager CacheManager;
        protected ConfigCenterEnvironmentCacheSqliteEFRepositoryImpl(ConfigCenterEnvironmentDBContext dbContext, ICacheManager cacheManager) : base(dbContext)
        {
            CacheManager = cacheManager;
        }
        /// <summary>
        /// 获得缓存键
        /// </summary>
        /// <returns></returns>
        public abstract string GetCacheKey();
        /// <summary>
        /// 缓存键
        /// </summary>
        public string CacheKey => GetCacheKey();

        public async Task<List<T>> GetAllInfoFromCacheAsync()
        {
            List<T> result = CacheManager.Get<List<T>>(CacheKey);
            if (result != null) return result;
            result = await FindAsync(m => true);
            CacheManager.SetByAbsolute(CacheKey, result, 1, DateTimeTypeEnum.Hour);
            return result;
        }

        public Task ClearCacheAsync()
        {
            if (CacheManager.GetCacheKeys().Contains(CacheKey))
            {
                CacheManager.Remove(CacheKey);
            }
            return Task.CompletedTask;
        }
    }
}
