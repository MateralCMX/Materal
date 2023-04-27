using Materal.TTA.Common;
using Materal.Utils.Cache;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// 缓存仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class CacheEFRepositoryImpl<T, TPrimaryKeyType, TDBContext> : EFRepositoryImpl<T, TPrimaryKeyType, TDBContext>, ICacheEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
        where TDBContext : DbContext
    {
        /// <summary>
        /// 缓存帮助类
        /// </summary>
        protected readonly ICacheHelper CacheHelper;
        /// <summary>
        /// 保存所有信息的缓存名称
        /// </summary>
        protected string AllInfoCacheName => GetAllCacheName();
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="cacheManager"></param>
        protected CacheEFRepositoryImpl(TDBContext dbContext, ICacheHelper cacheManager) : base(dbContext)
        {
            CacheHelper = cacheManager;
        }
        /// <summary>
        /// 获得所有缓存名称
        /// </summary>
        /// <returns></returns>
        protected abstract string GetAllCacheName();
        /// <summary>
        /// 从缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllInfoFromCacheAsync() => await GetInfoFromCacheAsync(AllInfoCacheName);
        /// <summary>
        /// 从缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<T> GetAllInfoFromCache()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        public virtual async Task ClearAllCacheAsync() => await ClearCacheAsync(AllInfoCacheName);
        /// <summary>
        /// 清理缓存
        /// </summary>
        public void ClearAllCache() => ClearCache(AllInfoCacheName);
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetInfoFromCacheAsync(string key)
        {
            List<T>? result = CacheHelper.GetOrDefault<List<T>>(key);
            if (result != null) return result;
            result = await FindAsync(m => true);
            CacheHelper.SetBySliding(key, result, 1);
            return result;
        }
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<T> GetInfoFromCache(string key)
        {
            List<T>? result = CacheHelper.GetOrDefault<List<T>>(key);
            if (result != null) return result;
            result = Find(m => true);
            CacheHelper.SetBySliding(key, result, 1);
            return result;
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Task ClearCacheAsync(string key)
        {
            ClearCache(key);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <param name="key"></param>
        public virtual void ClearCache(string key)
        {
            if (CacheHelper.KeyAny(key))
            {
                CacheHelper.Remove(key);
            }
        }
    }
}
