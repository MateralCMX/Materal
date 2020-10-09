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
        private static readonly List<string> _subscriberChannelNames = new List<string>();
        private static readonly object _subscriberLock = new object();
        private readonly RedisManager _redisManager;
        private readonly ICacheManager _cacheManager;
        private string _allInfoCacheName => GetAllCacheName();
        protected CacheEFRepositoryImpl(DbContext dbContext, RedisManager redisManager, ICacheManager cacheManager) : base(dbContext)
        {
            _redisManager = redisManager;
            _cacheManager = cacheManager;
            lock (_subscriberLock)
            {
                if (_subscriberChannelNames.Contains(_allInfoCacheName)) return;
                _subscriberChannelNames.Add(_allInfoCacheName);
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
            var result = _cacheManager.Get<List<T>>(_allInfoCacheName);
            if (result != null) return result;
            if (await _redisManager.StringExistsAsync(_allInfoCacheName))
            {
                result = await _redisManager.StringGetAsync<List<T>>(_allInfoCacheName);
                _cacheManager.SetBySliding(_allInfoCacheName, result, 1);
                return result;
            }
            result = await DBSet.Where(m => true).ToListAsync();
            _cacheManager.SetBySliding(_allInfoCacheName, result, 1);
            await _redisManager.StringSetAsync(_allInfoCacheName, result);
            return result;
        }
        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        public virtual async Task ClearCacheAsync()
        {
            await _redisManager.PublishAsync(_allInfoCacheName, null);
        }
        /// <summary>
        /// 订阅清理缓存事件
        /// </summary>
        /// <returns></returns>
        private async Task SubscriberAsync()
        {
            await _redisManager.SubscriberAsync(_allInfoCacheName, async (channelName, value) => await ClearCacheHandlerAsync(channelName));
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
