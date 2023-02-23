using Materal.TTA.Common;
using Materal.Utils.Cache;
using Materal.Utils.Redis;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.EFRepository
{
    public abstract class CacheEFRepositoryImpl<T, TPrimaryKeyType> : EFRepositoryImpl<T, TPrimaryKeyType>, ICacheEFRepository<T, TPrimaryKeyType>
        where T : class, IEntity<TPrimaryKeyType>, new()
        where TPrimaryKeyType : struct
    {
        protected static readonly List<string> SubscriberChannelNames = new();
        protected readonly RedisManager? _redisManager;
        protected readonly ICacheHelper _cacheManager;
        protected string AllInfoCacheName => GetAllCacheName();
        protected CacheEFRepositoryImpl(DbContext dbContext, ICacheHelper cacheManager, RedisManager? redisManager = null) : base(dbContext)
        {
            _redisManager = redisManager;
            _cacheManager = cacheManager;
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
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        public virtual async Task ClearAllCacheAsync() => await ClearCacheAsync(AllInfoCacheName);
        /// <summary>
        /// 订阅清理缓存事件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual async Task SubscriberAsync(string key)
        {
            if (_redisManager == null) return;
            await _redisManager.SubscriberAsync(key, async (channelName, value) =>
            {
                if (string.IsNullOrWhiteSpace(channelName)) return;
                await ClearCacheHandlerAsync(channelName);
            });
        }
        /// <summary>
        /// 清理缓存处理器
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        protected virtual async Task ClearCacheHandlerAsync(string channelName)
        {
            if (_redisManager != null && await _redisManager.StringExistsAsync(channelName))
            {
                await _redisManager.StringDeleteAsync(channelName);
            }
            if (_cacheManager.KeyAny(channelName))
            {
                _cacheManager.Remove(channelName);
            }
        }
        /// <summary>
        /// 通过缓存获得信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetInfoFromCacheAsync(string key)
        {
            List<T>? result = _cacheManager.GetOrDefault<List<T>>(key);
            if (result != null) return result;
            if (_redisManager != null && await _redisManager.StringExistsAsync(key))
            {
                result = await _redisManager.StringGetAsync<List<T>>(key);
                if (result == null) return new List<T>();
                _cacheManager.SetBySliding(key, result, 1);
                return result;
            }
            result = await DBSet.Where(m => true).ToListAsync();
            _cacheManager.SetBySliding(key, result, 1);
            if (_redisManager != null)
            {
                if (!SubscriberChannelNames.Contains(key))
                {
                    SubscriberChannelNames.Add(key);
                    await SubscriberAsync(key);
                }
                await _redisManager.StringSetAsync(key, result);
            }
            return result;
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task ClearCacheAsync(string key)
        {
            if (_redisManager != null)
            {
                await _redisManager.PublishAsync(key, null);
            }
            else if (_cacheManager.KeyAny(key))
            {
                _cacheManager.Remove(key);
            }
        }
    }
}
