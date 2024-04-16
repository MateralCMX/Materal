namespace Materal.Logger.MySqlLogger
{
    /// <summary>
    /// LoggerOptions扩展
    /// </summary>
    public static partial class LoggerOptionsExtensions
    {
        /// <summary>
        /// 添加一个MySql输出
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        /// <param name="tableName"></param>
        /// <param name="fileds"></param>
        public static LoggerOptions AddMySqlTarget(this LoggerOptions options, string name, string connectionString, string? tableName = null, List<MySqlFiled>? fileds = null)
        {
            MySqlLoggerTargetOptions target = new()
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
            options.AddTarget(target);
            return options;
        }
    }
}
