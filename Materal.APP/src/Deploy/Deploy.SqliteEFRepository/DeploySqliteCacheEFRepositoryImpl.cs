using Materal.APP.Core.Models;
using Materal.CacheHelper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.SqliteEFRepository
{
    public abstract class DeploySqliteCacheEFRepositoryImpl<T> : DeploySqliteEFRepositoryImpl<T> where T : BaseDomain, new()
    {
        protected readonly ICacheManager CacheManager;
        protected DeploySqliteCacheEFRepositoryImpl(DeployDBContext dbContext, ICacheManager cacheManager) : base(dbContext)
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
            if (CacheManager.GetCacheKeys().Contains(CacheKey))
            {
                return CacheManager.Get<List<T>>(CacheKey);
            }
            List<T> result = await FindAsync(m => true);
            CacheManager.SetByAbsolute(CacheKey, result, 1);
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
