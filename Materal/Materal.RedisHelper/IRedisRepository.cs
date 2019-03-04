using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Materal.RedisHelper
{
    public interface IRedisRepository
    {
        /// <summary>
        /// 哈希表存放
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="key">键</param>
        /// <param name="hashEntries">哈希值</param>
        /// <returns></returns>
        Task HashSetAsync(IDatabase database, string key, params HashEntry[] hashEntries);
        /// <summary>
        /// 哈希表存放
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="key">键</param>
        /// <param name="hashEntries">哈希值</param>
        void HashSet(IDatabase database, string key, params HashEntry[] hashEntries);
        /// <summary>
        /// 哈希表批量存放
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="pairs">键值集合</param>
        void HashSet(IDatabase database, Dictionary<string, HashEntry[]> pairs);
        /// <summary>
        /// 哈希表批量存放
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="pairs">键值集合</param>
        void HashSet(IDatabase database, Dictionary<List<string>, HashEntry> pairs);
        /// <summary>
        /// 哈希表获取
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="key">键</param>
        /// <param name="name">值</param>
        /// <returns></returns>
        Task<RedisValue> HashGetAsync(IDatabase database, string key, string name);
        /// <summary>
        /// 哈希表获取
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="key">键</param>
        /// <param name="name">值</param>
        /// <returns></returns>
        RedisValue HashGet(IDatabase database, string key, string name);
        /// <summary>
        /// 哈希表删除键
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task HashDeleteKeyAsync(IDatabase database, string key);
        /// <summary>
        /// 设置键值对
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetAsync(IDatabase database, string key, string value);
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<List<string>> GetAsync(IDatabase database, string key);
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="database"></param>
        /// <param name="keys"></param>
        /// <param name="value"></param>
        void Set(IDatabase database, List<Guid> keys, string value);
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        void Set(IDatabase database, string key, List<string> values);
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="database"></param>
        /// <param name="pairs"></param>
        void Set(IDatabase database, Dictionary<string, List<string>> pairs);
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="database"></param>
        /// <param name="pairs"></param>
        void Set(IDatabase database, Dictionary<List<string>, string> pairs);
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="database"></param>
        /// <param name="pairs"></param>
        void Set(IDatabase database, Dictionary<string, string> pairs);
        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task DeleteKeyAsync(IDatabase database, string key);
        /// <summary>
        /// 设置批量删除键
        /// </summary>
        /// <param name="database"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task DeleteKeyAsync(IDatabase database, List<string> keys);
        /// <summary>
        /// 设置删除值
        /// </summary>
        /// <param name="database"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task RemoveValueAsync(IDatabase database, string key, string value);
        /// <summary>
        /// 设置批量删除值
        /// </summary>
        /// <param name="database"></param>
        /// <param name="keys"></param>
        /// <param name="value"></param>
        void RemoveValueAsync(IDatabase database, List<string> keys, string value);
    }
}
