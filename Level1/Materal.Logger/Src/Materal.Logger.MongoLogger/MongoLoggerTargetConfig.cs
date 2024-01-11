namespace Materal.Logger.MongoLogger
{
    /// <summary>
    /// Mongo目标配置
    /// </summary>
    public class MongoLoggerTargetConfig : BatchLoggerTargetConfig<MongoLoggerWriter>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "Mongo";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; } = "mongodb://localhost:27017/";
        /// <summary>
        /// 数据库名
        /// </summary>
        public string DBName { get; set; } = "MateralLogger";
        /// <summary>
        /// 集合名
        /// </summary>
        public string CollectionName { get; set; } = "Logger${Year}${Month}${Day}";
    }
}
