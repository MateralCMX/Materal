namespace Materal.Logger.BusLogger
{
    /// <summary>
    /// 自定义日志配置
    /// </summary>
    public static class LoggerOptionsExtensions
    {
        /// <summary>
        /// 添加一个总线目标
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        public static LoggerOptions AddBusTarget(this LoggerOptions options, string name)
        {
            BusLoggerTargetOptions target = new()
            {
                Name = name
            };
            options.AddTarget(target);
            return options;
        }
    }
}
