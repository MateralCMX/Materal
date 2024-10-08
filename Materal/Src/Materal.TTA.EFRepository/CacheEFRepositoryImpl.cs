using Materal.Utils.Cache;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// 缓存仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class CacheEFRepositoryImpl<TEntity, TPrimaryKeyType, TDBContext>(TDBContext dbContext, ICacheHelper cacheManager) : EFRepositoryImpl<TEntity, TPrimaryKeyType, TDBContext>(dbContext), ICacheEFRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 缓存帮助类
        /// </summary>
        protected readonly ICacheHelper CacheHelper = cacheManager;
        /// <summary>
        /// 保存所有信息的缓存名称
        /// </summary>
        protected string AllInfoCacheName => GetAllCacheName();
        /// <summary>
        /// 获得所有缓存名称
        /// </summary>
        /// <returns></returns>
        protected abstract string GetAllCacheName();
        /// <summary>
        /// 从缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAllInfoFromCache() => GetInfoFromCache(AllInfoCacheName);
        /// <summary>
        /// 从缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllInfoFromCacheAsync() => await GetInfoFromCacheAsync(AllInfoCacheName);
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<TEntity> GetInfoFromCache(string key)
        {
            List<TEntity>? result = CacheHelper.GetOrDefault<List<TEntity>>(key);
            if (result != null) return result;
            result = Find(m => true);
            CacheHelper.SetBySliding(key, result, 1);
            return result;
        }
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetInfoFromCacheAsync(string key)
        {
            List<TEntity>? result = CacheHelper.GetOrDefault<List<TEntity>>(key);
            if (result != null) return result;
            result = await FindAsync(m => true);
            CacheHelper.SetBySliding(key, result, 1);
            return result;
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        public void ClearAllCache() => ClearCache(AllInfoCacheName);
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        public virtual async Task ClearAllCacheAsync() => await ClearCacheAsync(AllInfoCacheName);
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <param name="key"></param>
        public virtual void ClearCache(string key)
        {
            if (!CacheHelper.KeyAny(key)) return;
            CacheHelper.Remove(key);
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task ClearCacheAsync(string key)
        {
            ClearCache(key);
            await Task.CompletedTask;
        }
    }
}
