//using Materal.Logger.Models;
//using System.IO;

//namespace Materal.Logger
//{
//    /// <summary>
//    /// 日志配置选项扩展
//    /// </summary>
//    public static class LoggerConfigOptionsExtension
//    {
//        /// <summary>
//        /// 添加一个Sqlite输出[文件路径]
//        /// </summary>
//        /// <param name="loggerConfigOptions"></param>
//        /// <param name="name"></param>
//        /// <param name="path"></param>
//        public static LoggerConfigOptions AddSqliteTargetFromPath(this LoggerConfigOptions loggerConfigOptions, string name, string path)
//        {
//            SqliteLoggerTargetConfigModel target = new()
//            {
//                Name = name,
//                Path = path,
//            };
//            loggerConfigOptions.AddTarget(target);
//            return loggerConfigOptions;
//        }
//        /// <summary>
//        /// 添加一个Sqlite输出[链接字符串]
//        /// </summary>
//        /// <param name="loggerConfigOptions"></param>
//        /// <param name="name"></param>
//        /// <param name="connectionString"></param>
//        public static LoggerConfigOptions AddSqliteTargetFromConnectionString(this LoggerConfigOptions loggerConfigOptions, string name, string connectionString)
//        {
//            SqliteLoggerTargetConfigModel target = new()
//            {
//                Name = name,
//                ConnectionString = connectionString,
//            };
//            loggerConfigOptions.AddTarget(target);
//            return loggerConfigOptions;
//        }
//    }
//}
