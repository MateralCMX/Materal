namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项扩展
    /// </summary>
    public static class LoggerConfigOptionsExtension
    {
        /// <summary>
        /// 添加一个Mongo输出
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        public static LoggerConfigOptions AddMongoTarget(this LoggerConfigOptions loggerConfigOptions, string name, string connectionString, string? dbName = null, string? collectionName = null)
        {
            MongoLoggerTargetConfigModel target = new()
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
            loggerConfigOptions.AddTarget(target);
            return loggerConfigOptions;
        }
    }
}
