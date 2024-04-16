namespace Materal.Logger.MongoLogger
{
    /// <summary>
    /// LoggerOptions扩展
    /// </summary>
    public static class LoggerOptionsExtensions
    {
        /// <summary>
        /// 添加一个Mongo输出
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        public static LoggerOptions AddMongoTarget(this LoggerOptions options, string name, string connectionString, string? dbName = null, string? collectionName = null)
        {
            MongoLoggerTargetOptions target = new()
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
            options.AddTarget(target);
            return options;
        }
    }
}
