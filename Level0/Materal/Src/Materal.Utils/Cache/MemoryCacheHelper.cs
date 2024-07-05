using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Materal.Utils.Cache
{
    /// <summary>
    /// 内存缓存管理器
    /// </summary>
    public class MemoryCacheHelper(IMemoryCache? memoryCache = null) : ICacheHelper
    {
        private readonly List<string> allKey = [];
        private readonly IMemoryCache _memoryCache = memoryCache ?? new MemoryCache(Options.Create(new MemoryCacheOptions()));
        private readonly object _setValueLockObj = new();
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SetBySliding(string key, object content, double hours) => SetBySliding(key, content, hours, DateTimeTypeEnum.Hour);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SetBySliding(string key, object content, double timer, DateTimeTypeEnum dateTimeType)
        {
            double millisecond = DateTimeHelper.ToMilliseconds(timer, dateTimeType);
            SetBySliding(key, content, TimeSpan.FromMilliseconds(millisecond));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SetBySliding(string key, object content, DateTime date)
        {
            TimeSpan timeSpan = DateTime.Now - date;
            SetBySliding(key, content, timeSpan);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SetBySliding(string key, object content, TimeSpan timeSpan)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetSlidingExpiration(timeSpan);
            SetValue(key, content, options);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SetByAbsolute(string key, object content, double hours) => SetByAbsolute(key, content, hours, DateTimeTypeEnum.Hour);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SetByAbsolute(string key, object content, double timer, DateTimeTypeEnum dateTimeType)
        {
            double millisecond = DateTimeHelper.ToMilliseconds(timer, dateTimeType);
            SetByAbsolute(key, content, TimeSpan.FromMilliseconds(millisecond));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SetByAbsolute(string key, object content, DateTime date)
        {
            TimeSpan timeSpan = DateTime.Now - date;
            SetByAbsolute(key, content, timeSpan);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void SetByAbsolute(string key, object content, TimeSpan timeSpan)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetAbsoluteExpiration(timeSpan);
            SetValue(key, content, options);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public object Get(string key) => GetOrDefault(key) ?? throw new MateralException("值不存在");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public T Get<T>(string key) => GetOrDefault<T>(key) ?? throw new MateralException("值不存在");
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public object? GetOrDefault(string key) => _memoryCache.Get(key);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public T? GetOrDefault<T>(string key) => _memoryCache.Get<T>(key);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Remove(string key)
        {
            lock (_setValueLockObj)
            {
                allKey.Remove(key);
                _memoryCache.Remove(key);
            }
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<string> GetCacheKeys() => allKey.ToJson().JsonToObject<List<string>>() ?? [];
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool KeyAny(string key) => GetOrDefault(key) is not null;
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public void Clear()
        {
            string[]? tempAllKey = allKey.ToJson().JsonToObject<string[]>();
            if (tempAllKey is null || tempAllKey.Length <= 0) return;
            foreach (string item in tempAllKey)
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
