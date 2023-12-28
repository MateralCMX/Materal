using Materal.Logger.HttpLogger;

namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static partial class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个Http输出
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        public static LoggerConfig AddHttpTarget(this LoggerConfig loggerConfig, string name, string url, HttpMethod? httpMethod = null)
        {
            HttpLoggerTargetConfig target = new()
            {
                Name = name,
                Url = url
            };
            if (httpMethod is not null)
            {
                target.HttpMethod = httpMethod.Method;
            }
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
    }
}
