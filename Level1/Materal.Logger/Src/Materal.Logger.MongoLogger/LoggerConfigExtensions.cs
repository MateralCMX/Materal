namespace Materal.Logger.MongoLogger
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static partial class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个Mongo输出
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        public static LoggerConfig AddMongoTarget(this LoggerConfig loggerConfig, string name, string connectionString, string? dbName = null, string? collectionName = null)
        {
            MongoLoggerTargetConfig target = new()
            {
                Name = name,
                ConnectionString = connectionString
            };
            if (dbName is not null && !string.IsNullOrWhiteSpace(dbName))
            {
                target.DBName = dbName;
            }
            if (collectionName is not null && !string.IsNullOrWhiteSpace(collectionName))
            {
                target.CollectionName = collectionName;
            }
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
    }
}
