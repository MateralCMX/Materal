namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// Sqlite目标配置
    /// </summary>
    public class SqliteLoggerTargetConfig : DBLoggerTargetConfig<SqliteLoggerWriter, SqliteDBFiled>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public override string Type => "Sqlite";
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; } = "C:\\MateralLogger\\SqliteLogger.db";
        /// <summary>
        /// 连接字符串前缀
        /// </summary>
        public const string ConnectionPrefix = "Data Source=";
        /// <summary>
        /// 连接字符串
        /// </summary>
        public override string ConnectionString
        {
            get => $"{ConnectionPrefix}{Path}";
            set
            {
                if (value is null || string.IsNullOrWhiteSpace(value)) throw new LoggerException("连接字符串不能为空");
                if (!value.StartsWith(ConnectionPrefix)) throw new LoggerException("连接字符串错误");
                Path = value[ConnectionPrefix.Length..];
            }
        }
        private static readonly List<SqliteDBFiled> _defaultFileds =
        [
            new() { Name = "ID", Type = "TEXT", Value = "${LogID}", PK = true },
            new() { Name = "CreateTime", Type = "DATE", Value = "${DateTime}", Index = "DESC", IsNull = false },
            new() { Name = "Level", Type = "TEXT", Value = "${Level}" },
            new() { Name = "ProgressID", Type = "TEXT", Value = "${ProgressID}" },
            new() { Name = "ThreadID", Type = "TEXT", Value = "${ThreadID}" },
            new() { Name = "Scope", Type = "TEXT", Value = "${Scope}" },
            new() { Name = "MachineName", Type = "TEXT", Value = "${MachineName}" },
            new() { Name = "CategoryName", Type = "TEXT", Value = "${CategoryName}" },
            new() { Name = "Application", Type = "TEXT", Value = "${Application}" },
            new() { Name = "Message", Type = "TEXT", Value = "${Message}" },
            new() { Name = "Exception", Type = "TEXT", Value = "${Exception}" }
        ];
        /// <summary>
        /// 默认字段
        /// </summary>
        public override List<SqliteDBFiled> DefaultFileds => _defaultFileds;
    }
}
