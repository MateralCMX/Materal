using Materal.Logger.Models;

namespace Materal.Logger
{
    /// <summary>
    /// 日志配置选项扩展
    /// </summary>
    public static class LoggerConfigOptionsExtension
    {
        /// <summary>
        /// 添加一个Sqlite输出
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="connectionString"></param>
        public static LoggerConfigOptions AddSqliteTarget(this LoggerConfigOptions loggerConfigOptions, string name, string? path = null, string? connectionString = null)
        {
            if ((path is null || string.IsNullOrWhiteSpace(path)) && (connectionString is null || string.IsNullOrWhiteSpace(connectionString))) throw new LoggerException("path或connectionString必须包含一个");
            SqliteLoggerTargetConfigModel target = new()
            {
                Name = name,
                Path = path,
                ConnectionString = connectionString
            };
            loggerConfigOptions.AddTarget(target);
            return loggerConfigOptions;
        }
        /// <summary>
        /// 添加一个Sqlite输出[文件路径]
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        public static LoggerConfigOptions AddSqliteTargetFromPath(this LoggerConfigOptions loggerConfigOptions, string name, string? path = null)
            => AddSqliteTarget(loggerConfigOptions, name, path);
        /// <summary>
        /// 添加一个Sqlite输出[链接字符串]
        /// </summary>
        /// <param name="loggerConfigOptions"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        public static LoggerConfigOptions AddSqliteTargetFromConnectionString(this LoggerConfigOptions loggerConfigOptions, string name, string? connectionString = null)
            => AddSqliteTarget(loggerConfigOptions, name, null, connectionString);
    }
}
