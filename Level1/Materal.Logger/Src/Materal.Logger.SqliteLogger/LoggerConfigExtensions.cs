namespace Materal.Logger.SqliteLogger
{
    /// <summary>
    /// LoggerOptions扩展
    /// </summary>
    public static partial class LoggerOptionsExtensions
    {
        /// <summary>
        /// 添加一个Sqlite输出[文件路径]
        /// </summary>
        /// <param name="loggerOptions"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerOptions AddSqliteTargetFromPath(this LoggerOptions loggerOptions, string name, string path, string? tableName = null, List<SqliteFiled>? fileds = null)
        {
            SqliteLoggerTargetOptions target = new()
            {
                Name = name,
                Path = path
            };
            return loggerOptions.AddSqliteTarget(target, name, tableName, fileds);
        }
        /// <summary>
        /// 添加一个Sqlite输出[链接字符串]
        /// </summary>
        /// <param name="loggerOptions"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerOptions AddSqliteTargetFromConnectionString(this LoggerOptions loggerOptions, string name, string connectionString, string? tableName = null, List<SqliteFiled>? fileds = null)
        {
            SqliteLoggerTargetOptions target = new()
            {
                Name = name,
                ConnectionString = connectionString,
            };
            return loggerOptions.AddSqliteTarget(target, name, tableName, fileds);
        }
        /// <summary>
        /// 添加一个Sqlite输出
        /// </summary>
        /// <param name="loggerOptions"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static LoggerOptions AddSqliteTarget(this LoggerOptions loggerOptions, SqliteLoggerTargetOptions target)
        {
            loggerOptions.AddTarget(target);
            return loggerOptions;
        }
        /// <summary>
        /// 添加一个Sqlite目标
        /// </summary>
        /// <param name="loggerOptions"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        private static LoggerOptions AddSqliteTarget(this LoggerOptions loggerOptions, SqliteLoggerTargetOptions target, string name, string? tableName = null, List<SqliteFiled>? fileds = null)
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
            return loggerOptions.AddSqliteTarget(target);
        }
    }
}
