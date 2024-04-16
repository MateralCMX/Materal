using Materal.Logger.DBLogger;

namespace Materal.Logger.SqlServerLogger
{
    /// <summary>
    /// SqlServer目标配置
    /// </summary>
    public class SqlServerLoggerTargetOptions : DBLoggerTargetOptions<SqlServerFiled>
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public override string ConnectionString { get; set; } = "Data Source=.;Initial Catalog=LogDB;Persist Security Info=True;User ID=sa;Password=123456";
        private static readonly List<SqlServerFiled> _defaultFileds =
        [
            new() { Name = "ID", Type = "[uniqueidentifier]", Value = "${LogID}", PK = true },
            new() { Name = "CreateTime", Type = "[datetime2](7)", Value = "${DateTime}", Index = "DESC", IsNull = false },
            new() { Name = "Level", Type = "[nvarchar](50)", Value = "${Level}" },
            new() { Name = "ProgressID", Type = "[nvarchar](20)", Value = "${ProgressID}" },
            new() { Name = "ThreadID", Type = "[nvarchar](20)", Value = "${ThreadID}" },
            new() { Name = "Scope", Type = "[nvarchar](100)", Value = "${Scope}" },
            new() { Name = "MachineName", Type = "[nvarchar](100)", Value = "${MachineName}" },
            new() { Name = "CategoryName", Type = "[nvarchar](100)", Value = "${CategoryName}" },
            new() { Name = "Application", Type = "[nvarchar](50)", Value = "${Application}" },
            new() { Name = "Message", Type = "[nvarchar](Max)", Value = "${Message}" },
            new() { Name = "Exception", Type = "[nvarchar](Max)", Value = "${Exception}" }
        ];
        /// <summary>
        /// 默认字段
        /// </summary>
        public override List<SqlServerFiled> DefaultFileds => _defaultFileds;
    }
}
