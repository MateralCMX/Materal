namespace Materal.Logger.MongoLogger
{
    /// <summary>
    /// MongoLogger目标配置
    /// </summary>
    public class MongoLoggerTargetOptions : LoggerTargetOptions
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = "mongodb://localhost:27017/";
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName { get; set; } = "Logs";
        /// <summary>
        /// 集合名
        /// </summary>
        public string CollectionName { get; set; } = "Logs";
    }
}
