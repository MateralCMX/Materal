using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Materal.RedisHelper
{
    public class RedisLock : IAsyncDisposable, IDisposable
    {
        private readonly string _key;
        private readonly IDatabase _database;
        internal RedisLock(string key, IDatabase database)
        {
            _key = key;
            _database = database;
        }

        public void Dispose()
        {
            _database.LockRelease(_key, string.Empty);
        }

        public async ValueTask DisposeAsync()
        {
            await _database.LockReleaseAsync(_key, string.Empty);
        }
    }
}
