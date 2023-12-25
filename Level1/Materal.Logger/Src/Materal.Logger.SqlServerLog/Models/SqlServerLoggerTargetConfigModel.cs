namespace Materal.Logger.Models
{
    /// <summary>
    /// SqlServer日志目标配置模型
    /// </summary>
    public class SqlServerLoggerTargetConfigModel : LoggerTargetConfigModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "SqlServer";
        /// <summary>
        /// 获得日志处理器
        /// </summary>
        /// <paramref name="loggerRuntime"></paramref>
        public override ILoggerHandler GetLoggerHandler(LoggerRuntime loggerRuntime) => new SqlServerLoggerHandler(loggerRuntime);
        private string _connectionString = "Data Source=.;Initial Catalog=LogDB;Persist Security Info=True;User ID=sa;Password=123456";
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
        /// 表名
        /// </summary>
        public string TableName { get; set; } = "MateralLogger";
        /// <summary>
        /// 默认字段
        /// </summary>
        public static List<SqlServerDBFiled> DefaultFileds { get; } =
        [
            new(){ Name="ID", Type="[uniqueidentifier]", Value="${LogID}", PK = true },
            new(){ Name="CreateTime", Type="[datetime2](7)", Value="${DateTime}", Index = true, IsNull = false },
            new(){ Name="Level", Type="[nvarchar](50)", Value="${Level}" },
            new(){ Name="ProgressID", Type="[nvarchar](20)", Value="${ProgressID}" },
            new(){ Name="ThreadID", Type="[nvarchar](20)", Value="${ThreadID}" },
            new(){ Name="Scope", Type="[nvarchar](100)", Value="${Scope}" },
            new(){ Name="MachineName", Type="[nvarchar](100)", Value="${MachineName}" },
            new(){ Name="CategoryName", Type="[nvarchar](100)", Value="${CategoryName}" },
            new(){ Name="Application", Type="[nvarchar](50)", Value="${Application}" },
            new(){ Name="Message", Type="[nvarchar](Max)", Value="${Message}" },
            new(){ Name="Exception", Type="[nvarchar](Max)", Value="${Exception}" }
        ];
        /// <summary>
        /// 字段
        /// </summary>
        public List<SqlServerDBFiled> Fileds { get; set; } = [];
    }
}
