using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Materal.ConvertHelper;
using Materal.DateTimeHelper;
using StackExchange.Redis;

namespace Materal.RedisHelper
{
    public class RedisManager
    {
        private readonly string _connectionString;
        private ISubscriber _subscriber;
        /// <summary>
        /// Redis管理器
        /// </summary>
        /// <param name="redisConfig"></param>
        public RedisManager(RedisConfigModel redisConfig)
        {
            _connectionString = redisConfig.ConnectionString;
        }
        /// <summary>
        /// Redis管理器
        /// </summary>
        /// <param name="connectionString"></param>
        public RedisManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        #region Hash
        /// <summary>
        /// 设置Hash值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>是否成功</returns>
        public async Task<bool> HashSetAsync(string collectionName, string key, object value)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = ObjectToRedisValue(value);
            return await database.HashSetAsync(collectionName, key, redisValue);
        }
        /// <summary>
        /// 设置Hash值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="hashValues">要设置的值</param>
        /// <returns>是否成功</returns>
        public async Task HashSetAsync(string collectionName, ICollection<HashValue<object>> hashValues)
        {
            IDatabase database = GetDB(collectionName);
            HashEntry[] hashEntries = hashValues.Select(m => new HashEntry(m.Key, ObjectToRedisValue(m.Value))).ToArray();
            await database.HashSetAsync(collectionName, hashEntries);
        }
        /// <summary>
        /// 获得Hash值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string collectionName, string key)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = await database.HashGetAsync(collectionName, key);
            return RedisValueToObject<T>(redisValue);
        }
        /// <summary>
        /// 获得所有Hash值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<HashValue<T>[]> HashGetAllAsync<T>(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            HashEntry[] hashEntries = await database.HashGetAllAsync(collectionName);
            return hashEntries.Select(hashEntry => new HashValue<T>
            {
                Key = hashEntry.Name,
                Value = RedisValueToObject<T>(hashEntry.Value)
            }).ToArray();
        }
        /// <summary>
        /// 移除Hash值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="keys">键</param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string collectionName, ICollection<string> keys)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue[] redisValues = keys.Select(m => new RedisValue(m)).ToArray();
            return await database.HashDeleteAsync(collectionName, redisValues);
        }
        /// <summary>
        /// 移除Hash值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string collectionName, string key)
        {
            IDatabase database = GetDB(collectionName);
            return await database.HashDeleteAsync(collectionName, key);
        }
        /// <summary>
        /// 移除Hash
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string collectionName)
        {
            return await KeyDeleteAsync(collectionName);
        }
        /// <summary>
        /// Hash值是否存在
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string collectionName, string key)
        {
            IDatabase database = GetDB(collectionName);
            return await database.HashExistsAsync(collectionName, key);
        }
        /// <summary>
        /// Hash值是否存在
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string collectionName)
        {
            return await KeyExistsAsync(collectionName);
        }
        /// <summary>
        /// Hash值减少
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key">键</param>
        /// <param name="value">减少的值</param>
        /// <returns></returns>
        public async Task<long> HashDecrementAsync(string collectionName, string key, long value = 1)
        {
            IDatabase database = GetDB(collectionName);
            return await database.HashDecrementAsync(collectionName, key, value);
        }
        /// <summary>
        /// Hash值增加
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key">键</param>
        /// <param name="value">增加的值</param>
        /// <returns></returns>
        public async Task<long> HashIncrementAsync(string collectionName, string key, long value = 1)
        {
            IDatabase database = GetDB(collectionName);
            return await database.HashIncrementAsync(collectionName, key, value);
        }
        /// <summary>
        /// Hash长度
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<long> HashLengthAsync(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            return await database.HashLengthAsync(collectionName);
        }
        /// <summary>
        /// 获取Hash所有的键
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<string[]> HashAllKeysAsync(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue[] redisValues = await database.HashKeysAsync(collectionName);
            return redisValues.Select(m => m.ToString()).ToArray();
        }
        /// <summary>
        /// Hash值长度
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> HashStringLengthAsync(string collectionName, string key)
        {
            IDatabase database = GetDB(collectionName);
            return await database.HashStringLengthAsync(collectionName, key);
        }
        #endregion
        #region String
        /// <summary>
        /// 设置String值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> StringSetAsync(string collectionName, object value)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = ObjectToRedisValue(value);
            return await database.StringSetAsync(collectionName, redisValue);
        }
        /// <summary>
        /// 获取String值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = await database.StringGetAsync(collectionName);
            var result = RedisValueToObject<T>(redisValue);
            return result;
        }
        /// <summary>
        /// 追加String值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<long> StringAppendAsync(string collectionName, object value)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = ObjectToRedisValue(value);
            return await database.StringAppendAsync(collectionName, redisValue);
        }
        /// <summary>
        /// String是否存在
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<bool> StringExistsAsync(string collectionName)
        {
            return await KeyExistsAsync(collectionName);
        }
        /// <summary>
        /// 删除String
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<bool> StringDeleteAsync(string collectionName)
        {
            return await KeyDeleteAsync(collectionName);
        }
        /// <summary>
        /// String值减少
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="value">要减少的数量</param>
        /// <returns></returns>
        public async Task<long> StringDecrementAsync(string collectionName, long value = 1)
        {
            IDatabase database = GetDB(collectionName);
            return await database.StringDecrementAsync(collectionName, value);
        }
        /// <summary>
        /// String值增加
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="value">要增加的数量</param>
        /// <returns></returns>
        public async Task<long> StringIncrementAsync(string collectionName, long value = 1)
        {
            IDatabase database = GetDB(collectionName);
            return await database.StringIncrementAsync(collectionName, value);
        }
        /// <summary>
        /// String值长度
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<long> StringLengthAsync(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            return await database.StringLengthAsync(collectionName);
        }
        #endregion
        #region List
        /// <summary>
        /// 弹出List左边的值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = await database.ListLeftPopAsync(collectionName);
            var result = RedisValueToObject<T>(redisValue);
            return result;
        }
        /// <summary>
        /// 弹出List右边的值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = await database.ListRightPopAsync(collectionName);
            var result = RedisValueToObject<T>(redisValue);
            return result;
        }
        /// <summary>
        /// 获取List该位序的值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="index">位序</param>
        /// <returns></returns>
        public async Task<T> ListGetByIndexAsync<T>(string collectionName, long index)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = await database.ListGetByIndexAsync(collectionName, index);
            var result = RedisValueToObject<T>(redisValue);
            return result;
        }
        /// <summary>
        /// 向List左边添加
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string collectionName, object value)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = ObjectToRedisValue(value);
            return await database.ListLeftPushAsync(collectionName, redisValue);
        }
        /// <summary>
        /// 向List右边添加
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string collectionName, object value)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = ObjectToRedisValue(value);
            return await database.ListRightPushAsync(collectionName, redisValue);
        }
        /// <summary>
        /// 在List位序出更新值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="index">位序</param>
        /// <param name="value">值</param>
        public async Task ListSetByIndexAsync(string collectionName, int index, object value)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = ObjectToRedisValue(value);
            await database.ListSetByIndexAsync(collectionName, index, redisValue);
        }
        /// <summary>
        /// 移除List对象
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<long> ListRemoveAsync(string collectionName, object value)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = ObjectToRedisValue(value);
            return await database.ListRemoveAsync(collectionName, redisValue);
        }
        /// <summary>
        /// 获取List范围数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="startIndex">开始位序</param>
        /// <param name="endIndex">结束位序</param>
        /// <returns></returns>
        public async Task<T[]> ListRangeAsync<T>(string collectionName, long startIndex, long endIndex = -1)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue[] redisValues = await database.ListRangeAsync(collectionName, startIndex, endIndex);
            return redisValues.Select(RedisValueToObject<T>).ToArray();
        }
        /// <summary>
        /// 获取List所有数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<T[]> ListGetAllAsync<T>(string collectionName)
        {
            return await ListRangeAsync<T>(collectionName, 0);
        }
        /// <summary>
        /// List修剪
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="startIndex">开始位序</param>
        /// <param name="endIndex">结束位序</param>
        /// <returns></returns>
        public async Task ListTrimAsync(string collectionName, long startIndex, long endIndex = -1)
        {
            IDatabase database = GetDB(collectionName);
            await database.ListTrimAsync(collectionName, startIndex, endIndex);
        }
        /// <summary>
        /// List移除
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="index">位序</param>
        /// <returns></returns>
        public async Task ListRemoveAtAsync(string collectionName, long index)
        {
            await ListTrimAsync(collectionName, index, index);
        }
        /// <summary>
        /// List清空
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task ListClearAsync(string collectionName)
        {
            await ListTrimAsync(collectionName, 0);
        }
        /// <summary>
        /// List长度
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            return await database.ListLengthAsync(collectionName);
        }
        #endregion
        #region Set
        /// <summary>
        /// 向集合添加值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public async Task<bool> SetAddAsync(string collectionName, object value)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = ObjectToRedisValue(value);
            return await database.SetAddAsync(collectionName, redisValue);
        }
        /// <summary>
        /// 从集合获取一个值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<T> SetGetAsync<T>(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = await database.SetRandomMemberAsync(collectionName);
            return RedisValueToObject<T>(redisValue);
        }
        /// <summary>
        /// 从集合弹出一个值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<T> SetPopAsync<T>(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            RedisValue redisValue = await database.SetPopAsync(collectionName);
            return RedisValueToObject<T>(redisValue);
        }
        /// <summary>
        /// 集合长度
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<long> SetLengthAsync(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            return await database.SetLengthAsync(collectionName);
        }
        #endregion
        #region 发布订阅
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="action"></param>
        public async Task SubscriberAsync(string channelName, Action<string, string> action)
        {
            ISubscriber subscriber = GetSubscriber();
            await subscriber.SubscribeAsync(channelName, (redisChannel, redisValue) =>
            {
                action?.Invoke(redisChannel, redisValue);
            });
        }
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="channelName"></param>
        /// <param name="value"></param>
        public async Task PublishAsync(string channelName, object value)
        {
            ISubscriber subscriber = GetSubscriber();
            RedisValue redisValue = ObjectToRedisValue(value);
            await subscriber.PublishAsync(channelName, redisValue);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channelName"></param>
        public async Task UnsubscribeAsync(string channelName)
        {
            if (_subscriber == null) return;
            await _subscriber.UnsubscribeAsync(channelName);
        }
        /// <summary>
        /// 取消所有订阅
        /// </summary>
        public async Task UnsubscribeAllAsync()
        {
            if (_subscriber == null) return;
            await _subscriber.UnsubscribeAllAsync();
        }
        #endregion
        #region Key
        /// <summary>
        /// 集合是否存在
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            return await database.KeyExistsAsync(collectionName);
        }
        /// <summary>
        /// 移除键
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            return await database.KeyDeleteAsync(collectionName);
        }
        /// <summary>
        /// 获得键类型
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public async Task<RedisType> KeyTypeAsync(string collectionName)
        {
            IDatabase database = GetDB(collectionName);
            return await database.KeyTypeAsync(collectionName);
        }
        #region Expiry
        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public async Task<bool> SetExpiryAsync(string collectionName, TimeSpan expiry)
        {
            IDatabase database = GetDB(collectionName);
            if (await database.KeyExistsAsync(collectionName))
            {
                return await database.KeyExpireAsync(collectionName, expiry);
            }
            return false;
        }
        /// <summary>
        /// 设置String值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="timeValue">小时数</param>
        /// <returns></returns>
        public async Task<bool> SetExpiryAsync(string collectionName, DateTime timeValue)
        {
            if (timeValue < DateTime.Now) throw new RedisHelperException("目标时间已过期");
            TimeSpan expiry = timeValue - DateTime.Now;
            return await SetExpiryAsync(collectionName, expiry);
        }
        /// <summary>
        /// 设置String值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="hours">小时数</param>
        /// <returns></returns>
        public async Task<bool> SetExpiryAsync(string collectionName, int hours)
        {
            return await SetExpiryAsync(collectionName, hours, DateTimeTypeEnum.Hour);
        }
        /// <summary>
        /// 设置String值
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <param name="timeValue">时间点</param>
        /// <param name="dateTimeType"></param>
        /// <returns></returns>
        public async Task<bool> SetExpiryAsync(string collectionName, int timeValue, DateTimeTypeEnum dateTimeType)
        {
            var milliseconds = Convert.ToInt64(DateTimeManager.ToMilliseconds(timeValue, dateTimeType));
            var expiry = new TimeSpan(milliseconds * 10000);
            return await SetExpiryAsync(collectionName, expiry);
        }
        #endregion
        #endregion
        #region 阻塞锁
        /// <summary>
        /// 获得一个阻塞锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">过期时间</param>
        /// <returns></returns>
        public async Task<RedisLock> GetBlockingLockAsync(string key, TimeSpan timeout)
        {
            IDatabase database = GetDB(key);
            do
            {
                bool isLock = await database.LockTakeAsync(key, string.Empty, timeout);
                if (isLock)
                {
                    var result = new RedisLock(key, database);
                    return result;
                }
                await Task.Delay(500);
            } while (true);
        }
        /// <summary>
        /// 获得一个阻塞锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">过期时间</param>
        /// <returns></returns>
        public async Task<RedisLock> GetBlockingLockAsync(string key, DateTime timeout)
        {
            TimeSpan expiry = timeout - DateTime.Now;
            return await GetBlockingLockAsync(key, expiry);
        }
        /// <summary>
        /// 获得一个阻塞锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">过期时间</param>
        /// <param name="timeoutType"></param>
        /// <returns></returns>
        public async Task<RedisLock> GetBlockingLockAsync(string key, int timeout, DateTimeTypeEnum timeoutType)
        {
            var milliseconds = Convert.ToInt64(DateTimeManager.ToMilliseconds(timeout, timeoutType));
            var expiry = new TimeSpan(milliseconds * 10000);
            return await GetBlockingLockAsync(key, expiry);
        }
        /// <summary>
        /// 获得一个阻塞锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">过期时间</param>
        /// <returns></returns>
        public async Task<RedisLock> GetBlockingLockAsync(string key, int timeout)
        {
            return await GetBlockingLockAsync(key, timeout, DateTimeTypeEnum.Millisecond);
        }
        #endregion
        #region 非阻塞锁
        /// <summary>
        /// 获得一个非阻塞锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">过期时间</param>
        /// <returns></returns>
        public async Task<RedisLock> GetNonBlockingLockAsync(string key, TimeSpan timeout)
        {
            IDatabase database = GetDB(key);
            bool isLock = await database.LockTakeAsync(key, string.Empty, timeout);
            if (!isLock) return null;
            var result = new RedisLock(key, database);
            return result;
        }
        /// <summary>
        /// 获得一个非阻塞锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">过期时间</param>
        /// <returns></returns>
        public async Task<RedisLock> GetNonBlockingLockAsync(string key, DateTime timeout)
        {
            TimeSpan expiry = timeout - DateTime.Now;
            return await GetNonBlockingLockAsync(key, expiry);
        }
        /// <summary>
        /// 获得一个非阻塞锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">过期时间</param>
        /// <param name="timeoutType"></param>
        /// <returns></returns>
        public async Task<RedisLock> GetNonBlockingLockAsync(string key, int timeout, DateTimeTypeEnum timeoutType)
        {
            var milliseconds = Convert.ToInt64(DateTimeManager.ToMilliseconds(timeout, timeoutType));
            var expiry = new TimeSpan(milliseconds * 10000);
            return await GetNonBlockingLockAsync(key, expiry);
        }
        /// <summary>
        /// 获得一个非阻塞锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="timeout">过期时间</param>
        /// <returns></returns>
        public async Task<RedisLock> GetNonBlockingLockAsync(string key, int timeout)
        {
            return await GetNonBlockingLockAsync(key, timeout, DateTimeTypeEnum.Millisecond);
        }
        #endregion
        #region 私有方法
        /// <summary>
        /// 获得发布订阅
        /// </summary>
        /// <returns></returns>
        protected ISubscriber GetSubscriber()
        {
            return _subscriber ??= RedisHelper.GetSubscriber(_connectionString);
        }
        /// <summary>
        /// 获得数据库
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected IDatabase GetDB(string key)
        {
            int index = GetDBIndex(key);
            IDatabase database = RedisHelper.GetDb(_connectionString, index);
            return database;
        }
        /// <summary>
        /// 获得数据库索引
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected virtual int GetDBIndex(string key)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            var index = 0;
            for (var i = 0; i < 8421504 && i < keyBytes.Length; i++)
            {
                index += keyBytes[i];
            }
            return index % 16;
        }
        /// <summary>
        /// 转换为String
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual RedisValue ObjectToRedisValue(object obj)
        {
            if (obj is string stringObj)
            {
                return new RedisValue(stringObj);
            }
            return new RedisValue(obj.ToJson());
        }
        /// <summary>
        /// 获得对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        protected virtual T RedisValueToObject<T>(RedisValue redisValue)
        {
            string stringResult = RedisValueToString(redisValue);
            dynamic result;
            try
            {
                if (typeof(T) == typeof(string))
                {
                    result = stringResult;
                }
                else
                {
                    result = stringResult.JsonToObject<T>();
                }
            }
            catch (Exception)
            {
                result = stringResult.JsonToDeserializeObject<T>();
            }
            return result;
        }
        /// <summary>
        /// 获得字符串
        /// </summary>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        protected virtual string RedisValueToString(RedisValue redisValue)
        {
            return redisValue.ToString();
        }
        #endregion
    }
}
