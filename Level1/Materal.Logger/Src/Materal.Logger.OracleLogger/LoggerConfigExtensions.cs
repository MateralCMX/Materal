namespace Materal.Logger.OracleLogger
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static partial class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个Oracle输出
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerConfig AddOracleTarget(this LoggerConfig loggerConfig, string name, string connectionString, string? tableName = null, List<OracleDBFiled>? fileds = null)
        {
            OracleLoggerTargetConfig target = new()
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
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
    }
}
