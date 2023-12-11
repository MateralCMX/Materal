using Materal.Logger.LoggerHandlers;

namespace Materal.Logger.Models
{
    /// <summary>
    /// Mongo日志目标配置模型
    /// </summary>
    public class MongoLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "Mongo";
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        public override ILoggerHandler GetLoggerHandler(LoggerRuntime loggerRuntime) => new MongoLoggerHandler(loggerRuntime);
        private string _connectionString = "mongodb://localhost:27017/";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value)) throw new LoggerException("连接字符串不能为空");
                _connectionString = value;
            }
        }
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
