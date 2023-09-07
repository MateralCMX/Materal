using Materal.Logger.Models;

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
        public static LoggerConfigOptions AddSqlServerTarget(this LoggerConfigOptions loggerConfigOptions, string name, string connectionString)
        {
            SqlServerLoggerTargetConfigModel target = new()
            {
                Name = name,
                ConnectionString = connectionString
            };
            loggerConfigOptions.AddTarget(target);
            return loggerConfigOptions;
        }
    }
}
