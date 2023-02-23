using StackExchange.Redis;

namespace Materal.Utils.Redis
{
    /// <summary>
    /// Redis锁
    /// </summary>
    public class RedisLock : IAsyncDisposable, IDisposable
    {
        private readonly string _key;
        private readonly IDatabase _database;
        internal RedisLock(string key, IDatabase database)
        {
            _key = key;
            _database = database;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose() => _database.LockRelease(_key, string.Empty);
        /// <summary>
        /// 释放
        /// </summary>
        /// <returns></returns>
        public async ValueTask DisposeAsync() => await _database.LockReleaseAsync(_key, string.Empty);
    }
}
