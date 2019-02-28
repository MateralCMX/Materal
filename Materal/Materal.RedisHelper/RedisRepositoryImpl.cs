using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.RedisHelper
{
    public sealed class RedisRepositoryImpl : IRedisRepository
    {
        public async Task HashSetAsync(IDatabase database, string key, params HashEntry[] hashEntries)
        {
            await database.HashSetAsync(key, hashEntries);
        }

        public void HashSet(IDatabase database, string key, params HashEntry[] hashEntries)
        {
            database.HashSet(key, hashEntries);
        }

        public void HashSet(IDatabase database, Dictionary<string, HashEntry[]> pairs)
        {
            var tasks = new List<Task>();
            foreach (KeyValuePair<string, HashEntry[]> item in pairs)
            {
                tasks.Add(database.HashSetAsync(item.Key, item.Value));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public void HashSet(IDatabase database, Dictionary<List<string>, HashEntry> pairs)
        {
            var tasks = new List<Task>();
            foreach (KeyValuePair<List<string>, HashEntry> item in pairs)
            {
                foreach (string key in item.Key)
                {
                    tasks.Add(database.HashSetAsync(key, new[] { item.Value }));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async Task<RedisValue> HashGetAsync(IDatabase database, string key, string name)
        {
            if (!database.HashExists(key, name)) return RedisValue.EmptyString;
            RedisValue redisValue = await database.HashGetAsync(key, name);
            return redisValue;
        }

        public RedisValue HashGet(IDatabase database, string key, string name)
        {
            if (!database.HashExists(key, name)) return RedisValue.EmptyString;
            RedisValue redisValue = database.HashGet(key, name);
            return redisValue;
        }

        public async Task HashDeleteKeyAsync(IDatabase database, string key)
        {
            await database.KeyDeleteAsync(key);
        }

        public async Task SetAsync(IDatabase database, string key, string value)
        {
            await database.SetAddAsync(key, value);
        }

        public async Task<List<string>> GetAsync(IDatabase database, string key)
        {
            if (string.IsNullOrEmpty(key)) return new List<string>();
            RedisValue[] value = await database.SetMembersAsync(key);
            return value.Select(q => (string)q).ToList();
        }

        public void Set(IDatabase database, List<Guid> keys, string value)
        {
            var tasks = new List<Task>();
            foreach (Guid item in keys)
            {
                tasks.Add(database.SetAddAsync(item.ToString(), value));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public void Set(IDatabase database, string key, List<string> values)
        {
            var tasks = new List<Task>();
            foreach (string item in values)
            {
                tasks.Add(database.SetAddAsync(key, item));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public void Set(IDatabase database, Dictionary<string, List<string>> pairs)
        {
            var tasks = new List<Task>();
            foreach (KeyValuePair<string, List<string>> item in pairs)
            {
                foreach (string value in item.Value)
                {
                    tasks.Add(database.SetAddAsync(item.Key, value));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }

        public void Set(IDatabase database, Dictionary<List<string>, string> pairs)
        {
            var tasks = new List<Task>();
            foreach (KeyValuePair<List<string>, string> item in pairs)
            {
                foreach (string key in item.Key)
                {
                    tasks.Add(database.SetAddAsync(key, item.Value));
                }
            }

            Task.WaitAll(tasks.ToArray());
        }

        public void Set(IDatabase database, Dictionary<string, string> pairs)
        {
            var tasks = new List<Task>();
            foreach (KeyValuePair<string, string> item in pairs)
            {
                tasks.Add(database.SetAddAsync(item.Key, item.Value));
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async Task DeleteKeyAsync(IDatabase database, string key)
        {
            await database.KeyDeleteAsync(key);
        }

        public async Task DeleteKeyAsync(IDatabase database, List<string> keys)
        {
            await database.KeyDeleteAsync(keys.Select(q => (RedisKey)q).ToArray());
        }

        public async Task RemoveValueAsync(IDatabase database, string key, string value)
        {
            if (string.IsNullOrEmpty(value)) return;

            await database.SetRemoveAsync(key, value);
        }

        public void RemoveValueAsync(IDatabase database, List<string> keys, string value)
        {
            var tasks = new List<Task>();
            foreach (string item in keys)
            {
                tasks.Add(database.SetRemoveAsync(item, value));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
