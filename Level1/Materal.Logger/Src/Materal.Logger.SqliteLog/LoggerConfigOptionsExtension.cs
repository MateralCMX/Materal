using Materal.Logger.Models;

namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项扩展
    /// </summary>
    public static class LoggerConfigOptionsExtension
    {
        /// <summary>
        /// 添加一个Sqlite输出[文件路径]
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerConfigOptions AddSqliteTargetFromPath(this LoggerConfigOptions loggerConfigOptions, string name, string path, string? tableName = null, List<SqliteDBFiled>? fileds = null)
        {
            SqliteLoggerTargetConfigModel target = new()
            {
                Name = name,
                Path = path
            };
            return loggerConfigOptions.AddSqliteTarget(target, name, tableName, fileds);
        }
        /// <summary>
        /// 添加一个Sqlite输出[链接字符串]
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerConfigOptions AddSqliteTargetFromConnectionString(this LoggerConfigOptions loggerConfigOptions, string name, string connectionString, string? tableName = null, List<SqliteDBFiled>? fileds = null)
        {
            SqliteLoggerTargetConfigModel target = new()
            {
                Name = name,
                ConnectionString = connectionString,
            };
            return loggerConfigOptions.AddSqliteTarget(target, name, tableName, fileds);
        }
        /// <summary>
        /// 添加一个Sqlite输出
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static LoggerConfigOptions AddSqliteTarget(this LoggerConfigOptions loggerConfigOptions, SqliteLoggerTargetConfigModel target)
        {
            loggerConfigOptions.AddTarget(target);
            return loggerConfigOptions;
        }
        /// <summary>
        /// 添加一个Sqlite目标
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="target"></param>
        /// <param name="name"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        private static LoggerConfigOptions AddSqliteTarget(this LoggerConfigOptions loggerConfigOptions, SqliteLoggerTargetConfigModel target, string name, string? tableName = null, List<SqliteDBFiled>? fileds = null)
        {
            target.Name = name;
            if(tableName is not null && !string.IsNullOrWhiteSpace(tableName))
            {
                target.TableName = tableName;
            }
            if (fileds is not null && fileds.Count > 0)
            {
                target.Fileds = fileds;
            }
            return loggerConfigOptions.AddSqliteTarget(target);
        }
    }
}
