//using Microsoft.Extensions.Logging;

//namespace Materal.Logger.Extensions
//{
//    /// <summary>
//    /// 日志扩展
//    /// </summary>
//    public static class ILoggerExtension
//    {
//        /// <summary>
//        /// 添加自定义配置
//        /// </summary>
//        /// <param name="_"></param>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        public static void AddCustomConfig(this ILogger _, string key, string value)
//        {
//            if (!LoggerConfig.CustomConfig.ContainsKey(key))
//            {
//                LoggerConfig.CustomConfig.Add(key, value);
//            }
//            else
//            {
//                LoggerConfig.CustomConfig[key] = value;
//            }
//        }
//        /// <summary>
//        /// 添加自定义配置
//        /// </summary>
//        /// <param name="_"></param>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        public static void AddCustomData(this ILogger _, string key, string value)
//        {
//            if (!LoggerConfig.CustomData.ContainsKey(key))
//            {
//                LoggerConfig.CustomData.Add(key, value);
//            }
//            else
//            {
//                LoggerConfig.CustomData[key] = value;
//            }
//        }
//    }
//}
