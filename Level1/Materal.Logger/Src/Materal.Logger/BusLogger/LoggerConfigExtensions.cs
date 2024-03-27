namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 日志配置扩展
    /// </summary>
    public static class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个总线目标
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        public static LoggerConfig AddBusTarget(this LoggerConfig loggerConfig, string name)
        {
            BusLoggerTargetConfig target = new()
            {
                Name = name
            };
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
    }
}
