namespace Materal.Logger.FileLogger
{
    /// <summary>
    /// 自定义日志配置
    /// </summary>
    public static class LoggerOptionsExtensions
    {
        /// <summary>
        /// 添加一个文件输出目标
        /// </summary>
        /// <param name="options"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        public static LoggerOptions AddFileTarget(this LoggerOptions options, string name, string path, string? format = null)
        {
            FileLoggerTargetOptions target = new()
            {
                Name = name,
                Path = path
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            options.AddTarget(target);
            return options;
        }
    }
}
