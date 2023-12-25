namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项扩展
    /// </summary>
    public static class LoggerConfigOptionsExtension
    {
        /// <summary>
        /// 添加一个SqlServer输出
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerConfigOptions AddSqlServerTarget(this LoggerConfigOptions loggerConfigOptions, string name, string connectionString, string? tableName = null, List<SqlServerDBFiled>? fileds = null)
        {
            SqlServerLoggerTargetConfigModel target = new()
            {
                Name = name,
                ConnectionString = connectionString
            };
            if (tableName is not null && !string.IsNullOrWhiteSpace(tableName))
            {
                target.TableName = tableName;
            }
            if (fileds is not null && fileds.Count > 0)
            {
                target.Fileds = fileds;
            }
            loggerConfigOptions.AddTarget(target);
            return loggerConfigOptions;
        }
    }
}
