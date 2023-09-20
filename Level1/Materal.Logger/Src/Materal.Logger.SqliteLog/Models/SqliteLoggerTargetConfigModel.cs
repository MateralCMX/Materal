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
        /// <param name="loggerRuntime"></param>
        public override ILoggerHandler GetLoggerHandler(LoggerRuntime loggerRuntime) => new SqliteLoggerHandler(loggerRuntime);
        private string _path = "C:\\MateralLogger\\SqliteLogger.db";
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
        private const string connectionPrefix = "Data Source=";
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
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; } = "MateralLogger";
        /// <summary>
        /// 默认字段
        /// </summary>
        public static List<SqliteDBFiled> DefaultFileds { get; } = new()
        {
            new(){ Name="ID", Type="TEXT", Value="${LogID}", PK = true },
            new(){ Name="CreateTime", Type="DATE", Value="${DateTime}", Index = true, IsNull = false },
            new(){ Name="Level", Type="TEXT", Value="${Level}" },
            new(){ Name="ProgressID", Type="TEXT", Value="${ProgressID}" },
            new(){ Name="ThreadID", Type="TEXT", Value="${ThreadID}" },
            new(){ Name="Scope", Type="TEXT", Value="${Scope}" },
            new(){ Name="MachineName", Type="TEXT", Value="${MachineName}" },
            new(){ Name="CategoryName", Type="TEXT", Value="${CategoryName}" },
            new(){ Name="Application", Type="TEXT", Value="${Application}" },
            new(){ Name="Message", Type="TEXT", Value="${Message}" },
            new(){ Name="Exception", Type="TEXT", Value="${Exception}" }
        };
        /// <summary>
        /// 字段
        /// </summary>
        public List<SqliteDBFiled> Fileds { get; set; } = new();
    }
}
