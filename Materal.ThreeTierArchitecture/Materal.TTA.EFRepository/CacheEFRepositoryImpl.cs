using Materal.CacheHelper;
using Materal.RedisHelper;
using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.TTA.EFRepository
{
    public abstract class CacheEFRepositoryImpl<T, TKey> : EFRepositoryImpl<T, TKey> where T : class, IEntity<TKey>, new()
    {
        private static readonly List<string> SubscriberChannelNames = new List<string>();
        private static readonly object SubscriberLock = new object();
        private readonly RedisManager _redisManager;
        private readonly ICacheManager _cacheManager;
        private string AllInfoCacheName => GetAllCacheName();
        protected CacheEFRepositoryImpl(DbContext dbContext, RedisManager redisManager, ICacheManager cacheManager) : base(dbContext)
        {
            _redisManager = redisManager;
            _cacheManager = cacheManager;
            lock (SubscriberLock)
            {
                if (SubscriberChannelNames.Contains(AllInfoCacheName)) return;
                SubscriberChannelNames.Add(AllInfoCacheName);
                Task subscriberTask = Task.Run(async () => await SubscriberAsync());
                Task.WaitAll(subscriberTask);
            }
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
        public virtual async Task<List<T>> GetAllInfoFromCacheAsync()
        {
            var result = _cacheManager.Get<List<T>>(AllInfoCacheName);
            if (result != null) return result;
            if (await _redisManager.StringExistsAsync(AllInfoCacheName))
            {
                result = await _redisManager.StringGetAsync<List<T>>(AllInfoCacheName);
                _cacheManager.SetBySliding(AllInfoCacheName, result, 1);
                return result;
            }
            result = await DBSet.Where(m => true).ToListAsync();
            _cacheManager.SetBySliding(AllInfoCacheName, result, 1);
            await _redisManager.StringSetAsync(AllInfoCacheName, result);
            return result;
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        public virtual async Task ClearCacheAsync()
        {
            await _redisManager.PublishAsync(AllInfoCacheName, null);
        }
        /// <summary>
        /// 订阅清理缓存事件
        /// </summary>
        /// <returns></returns>
        private async Task SubscriberAsync()
        {
            await _redisManager.SubscriberAsync(AllInfoCacheName, async (channelName, value) => await ClearCacheHandlerAsync(channelName));
        }
        /// <summary>
        /// 清理缓存处理器
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        private async Task ClearCacheHandlerAsync(string channelName)
        {
            if (await _redisManager.StringExistsAsync(channelName))
            {
                await _redisManager.StringDeleteAsync(channelName);
            }
            if (_cacheManager.GetCacheKeys().Contains(channelName))
            {
                _cacheManager.Remove(channelName);
            }
        }
    }
}
