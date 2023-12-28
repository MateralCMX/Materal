using Materal.Logger.SqliteLogger;

namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static partial class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个Sqlite输出[文件路径]
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerConfig AddSqliteTargetFromPath(this LoggerConfig loggerConfig, string name, string path, string? tableName = null, List<SqliteDBFiled>? fileds = null)
        {
            SqliteLoggerTargetConfig target = new()
            {
                Name = name,
                Path = path
            };
            return loggerConfig.AddSqliteTarget(target, name, tableName, fileds);
        }
        /// <summary>
        /// 添加一个Sqlite输出[链接字符串]
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerConfig AddSqliteTargetFromConnectionString(this LoggerConfig loggerConfig, string name, string connectionString, string? tableName = null, List<SqliteDBFiled>? fileds = null)
        {
            SqliteLoggerTargetConfig target = new()
            {
                Name = name,
                ConnectionString = connectionString,
            };
            return loggerConfig.AddSqliteTarget(target, name, tableName, fileds);
        }
        /// <summary>
        /// 添加一个Sqlite输出
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static LoggerConfig AddSqliteTarget(this LoggerConfig loggerConfig, SqliteLoggerTargetConfig target)
        {
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
        /// <summary>
        /// 添加一个Sqlite目标
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        private static LoggerConfig AddSqliteTarget(this LoggerConfig loggerConfig, SqliteLoggerTargetConfig target, string name, string? tableName = null, List<SqliteDBFiled>? fileds = null)
        {
            target.Name = name;
            if (tableName is not null && !string.IsNullOrWhiteSpace(tableName))
            {
                target.TableName = tableName;
            }
            if (fileds is not null && fileds.Count > 0)
            {
                target.Fileds = fileds;
            }
            return loggerConfig.AddSqliteTarget(target);
        }
    }
}
