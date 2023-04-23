using StackExchange.Redis;
using System.Collections.Concurrent;

namespace Materal.RedisHelper
{
    public class RedisHelper
    {
        private static readonly ConcurrentDictionary<string, ConnectionMultiplexer> Redis = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        /// <summary>
        /// 获得订阅
        /// </summary>
        /// <param name="redisConfig"></param>
        /// <returns></returns>
        public static ISubscriber GetSubscriber(RedisConfigModel redisConfig)
        {
            return GetSubscriber(redisConfig.ConnectionString);
        }
        /// <summary>
        /// 获得订阅
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static ISubscriber GetSubscriber(string connectionString)
        {
            ConnectionMultiplexer connection;
            if (Redis.ContainsKey(connectionString))
            {
                connection = Redis[connectionString];
                if (!connection.IsConnected)
                {
                    connection = NewConnectionMultiplexer(connectionString);
                }
            }
            else
            {
                connection = NewConnectionMultiplexer(connectionString);
            }
            ISubscriber result = connection.GetSubscriber();
            return result;
        }
        /// <summary>
        /// 获得数据库
        /// </summary>
        /// <param name="redisConfig"></param>
        /// <param name="dbIndex"></param>
        /// <returns></returns>
        public static IDatabase GetDb(RedisConfigModel redisConfig, int dbIndex = 0)
        {
            return GetDb(redisConfig.ConnectionString, dbIndex);
        }
        /// <summary>
        /// 获得数据库
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="dbIndex"></param>
        /// <returns></returns>
        public static IDatabase GetDb(string connectionString, int dbIndex = 0)
        {
            ConnectionMultiplexer connection;
            if (Redis.ContainsKey(connectionString))
            {
                connection = Redis[connectionString];
                if (!connection.IsConnected)
                {
                    connection = NewConnectionMultiplexer(connectionString);
                }
            }
            else
            {
                connection = NewConnectionMultiplexer(connectionString);
            }
            IDatabase result = connection.GetDatabase(dbIndex);
            return result;
        }
        #region 私有方法
        /// <summary>
        /// 获得新连接多路复用
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static ConnectionMultiplexer NewConnectionMultiplexer(string connectionString)
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(connectionString);
            Redis[connectionString] = connection;
            return connection;
        }
        #endregion
    }
}
