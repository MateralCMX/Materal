using StackExchange.Redis;
using System.Collections.Concurrent;

namespace Common
{
    public static class RedisHelper
    {
        private static volatile ConcurrentDictionary<string, ConnectionMultiplexer> _redis = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        public static IDatabase GetDb(string connectionString, int db = 0)
        {
            ConnectionMultiplexer connection;
            if (_redis.ContainsKey(connectionString))
            {
                connection = _redis[connectionString];
                if (connection.IsConnected) return connection.GetDatabase(db);
                connection = ConnectionMultiplexer.Connect(connectionString);
                _redis[connectionString] = connection;
            }
            else
            {
                connection = ConnectionMultiplexer.Connect(connectionString);
                _redis[connectionString] = connection;
            }
            return connection.GetDatabase(db);
        }
    }
}
