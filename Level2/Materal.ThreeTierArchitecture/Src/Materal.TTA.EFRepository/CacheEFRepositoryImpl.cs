using Materal.TTA.Common;
using Materal.Utils.Cache;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// 缓存仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKeyType"></typeparam>
    /// <typeparam name="TDBContext"></typeparam>
    public abstract class CacheEFRepositoryImpl<TEntity, TPrimaryKeyType, TDBContext> : EFRepositoryImpl<TEntity, TPrimaryKeyType, TDBContext>, ICacheEFRepository<TEntity, TPrimaryKeyType>
        where TEntity : class, IEntity<TPrimaryKeyType>, new()
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
        public virtual async Task<List<TEntity>> GetAllInfoFromCacheAsync() => await GetInfoFromCacheAsync(AllInfoCacheName);
        /// <summary>
        /// 从缓存获得所有信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<TEntity> GetAllInfoFromCache()
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
        public virtual async Task<List<TEntity>> GetInfoFromCacheAsync(string key)
        {
            List<TEntity>? result = CacheHelper.GetOrDefault<List<TEntity>>(key);
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
        public List<TEntity> GetInfoFromCache(string key)
        {
            List<TEntity>? result = CacheHelper.GetOrDefault<List<TEntity>>(key);
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
