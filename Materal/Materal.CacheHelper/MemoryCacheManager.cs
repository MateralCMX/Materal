using Materal.DateTimeHelper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Materal.CacheHelper
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        }

        public MemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void SetBySliding(string key, object content, double hours)
        {
            SetBySliding(key, content, hours, DateTimeTypeEnum.Hour);
        }

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
            _memoryCache.Set(key, content, options);
        }

        public void SetByAbsolute(string key, object content, double hours)
        {
            SetByAbsolute(key, content, hours, DateTimeTypeEnum.Hour);
        }

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
            _memoryCache.Set(key, content, options);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)Get(key);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
        public List<string> GetCacheKeys()
        {
            FieldInfo field = _memoryCache.GetType().GetField("_entries", BindingFlags.Instance | BindingFlags.NonPublic);
            var stringList = new List<string>();
            if (!(field?.GetValue(_memoryCache) is IDictionary dictionary)) return stringList;

            foreach (DictionaryEntry dictionaryEntry in dictionary)
            {
                stringList.Add(dictionaryEntry.Key.ToString());
            }
            return stringList;
        }
        public void Clear()
        {
            foreach (string cacheKey in GetCacheKeys())
            {
                Remove(cacheKey);
            }
        }
    }
}
