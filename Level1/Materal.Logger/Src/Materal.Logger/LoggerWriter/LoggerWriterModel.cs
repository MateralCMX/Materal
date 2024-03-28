namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 日志处理器模型
    /// </summary>
    public class LoggerWriterModel(LoggerConfig config, string? categoryName, LogLevel logLevel, string message, Exception? exception = null, LoggerScope? scope = null) : LogModel(categoryName, logLevel, message, exception, scope)
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="model"></param>
        public LoggerWriterModel(LoggerWriterModel model) : this(model.Config, model.CategoryName, model.LogLevel, model.Message, model.Exception, model.Scope)
        {
        }
        /// <summary>
        /// 日志配置
        /// </summary>
        public LoggerConfig Config { get; set; } = config;
    }
}
