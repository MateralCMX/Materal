using Materal.Logger.MySqlLogger;

namespace Materal.Logger
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个MySql输出
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerConfig AddMySqlTarget(this LoggerConfig loggerConfig, string name, string connectionString, string? tableName = null, List<MySqlDBFiled>? fileds = null)
        {
            MySqlLoggerTargetConfig target = new()
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
