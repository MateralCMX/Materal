using Materal.Logger.LoggerHandlers;

namespace Materal.Logger.Models
{
    /// <summary>
    /// Sqlite日志目标配置模型
    /// </summary>
    public class SqliteLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "Sqlite";
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        public override ILoggerHandler GetLoggerHandler() => new SqliteLoggerHandler();
        private string _path = "C:\\MateralLogger\\SqliteLogger.db";
        private const string connectionPrefix = "Data Source=";
        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value)) throw new LoggerException("路径格式不能为空");
                if (!value.IsRelativePath() && !value.IsAbsolutePath()) throw new LoggerException("路径格式错误");
                _path = value;
            }
        }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get => $"{connectionPrefix}{Path}";
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value)) throw new LoggerException("连接字符串不能为空");
                if (!value.StartsWith(connectionPrefix)) throw new LoggerException("连接字符串错误");
                Path = value[connectionPrefix.Length..];
            }
        }
    }
}
