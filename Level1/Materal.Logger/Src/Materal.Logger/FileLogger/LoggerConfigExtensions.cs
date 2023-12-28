using Materal.Logger.FileLogger;

namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// LoggerConfig扩展
    /// </summary>
    public static partial class LoggerConfigExtensions
    {
        /// <summary>
        /// 添加一个文件输出
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="format"></param>
        public static LoggerConfig AddFileTarget(this LoggerConfig loggerConfig, string name, string path, string? format = null)
        {
            FileLoggerTargetConfig target = new()
            {
                Name = name,
                Path = path
            };
            if (format is not null && !string.IsNullOrWhiteSpace(format))
            {
                target.Format = format;
            }
            loggerConfig.AddTarget(target);
            return loggerConfig;
        }
    }
}
