namespace Materal.Logger.HttpLogger
{
    /// <summary>
    /// 自定义日志配置
    /// </summary>
    public static class LoggerOptionsExtensions
    {
        /// <summary>
        /// 添加一个Http输出目标
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="httpMethod"></param>
        public static LoggerOptions AddHttpTarget(this LoggerOptions options, string name, string url, HttpMethod? httpMethod = null)
        {
            HttpLoggerTargetOptions target = new()
            {
                Name = name,
                Url = url
            };
            if (httpMethod is not null)
            {
                target.HttpMethod = httpMethod.Method;
            }
            options.AddTarget(target);
            return options;
        }
    }
}
