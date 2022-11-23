using Materal.Common;
using Materal.ConvertHelper;
using Materal.DateTimeHelper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Materal.CacheHelper
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly List<string> allKey = new();
        private readonly IMemoryCache _memoryCache;
        private readonly object _setValueLockObj = new();
        public MemoryCacheManager(IMemoryCache? memoryCache = null)
        {
            _memoryCache = memoryCache ?? new MemoryCache(Options.Create(new MemoryCacheOptions()));
        }
        public void SetBySliding(string key, object content, double hours) => SetBySliding(key, content, hours, DateTimeTypeEnum.Hour);
        public void SetBySliding(string key, object content, double timer, DateTimeTypeEnum dateTimeType)
        {
            double millisecond = DateTimeManager.ToMilliseconds(timer, dateTimeType);
            SetBySliding(key, content, TimeSpan.FromMilliseconds(millisecond));
        }
        public void SetBySliding(string key, object content, DateTime date)
        {
            TimeSpan timeSpan = DateTime.Now - date;
            SetBySliding(key, content, timeSpan);
        }
        public void SetBySliding(string key, object content, TimeSpan timeSpan)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetSlidingExpiration(timeSpan);
            SetValue(key, content, options);
        }
        public void SetByAbsolute(string key, object content, double hours) => SetByAbsolute(key, content, hours, DateTimeTypeEnum.Hour);
        public void SetByAbsolute(string key, object content, double timer, DateTimeTypeEnum dateTimeType)
        {
            double millisecond = DateTimeManager.ToMilliseconds(timer, dateTimeType);
            SetByAbsolute(key, content, TimeSpan.FromMilliseconds(millisecond));
        }
        public void SetByAbsolute(string key, object content, DateTime date)
        {
            TimeSpan timeSpan = DateTime.Now - date;
            SetByAbsolute(key, content, timeSpan);
        }
        public void SetByAbsolute(string key, object content, TimeSpan timeSpan)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(timeSpan);
            SetValue(key, content, options);
        }
        public object Get(string key) => GetOrDefault(key) ?? throw new MateralException("值不存在");
        public T Get<T>(string key) => GetOrDefault<T>(key) ?? throw new MateralException("值不存在");
        public object? GetOrDefault(string key) => _memoryCache.Get(key);
        public T? GetOrDefault<T>(string key) => _memoryCache.Get<T>(key);
        public void Remove(string key)
        {
            lock (_setValueLockObj)
            {
                allKey.Remove(key);
                _memoryCache.Remove(key);
            }
        }
        public List<string> GetCacheKeys() => allKey.ToJson().JsonToDeserializeObject<List<string>>() ?? new();
        public bool KeyAny(string key) => GetOrDefault(key) != null;
        public void Clear()
        {
            string[]? tempAllKey = allKey.ToJson().JsonToDeserializeObject<string[]>();
            if (tempAllKey == null || tempAllKey.Length <= 0) return;
            foreach (var item in tempAllKey)
            {
                Remove(item);
            }
        }
        private void SetValue(string key, object content, MemoryCacheEntryOptions options)
        {
            lock (_setValueLockObj)
            {
                if (!KeyAny(key))
                {
                    allKey.Add(key);
                }
                _memoryCache.Set(key, content, options);
            }
        }
    }
}
