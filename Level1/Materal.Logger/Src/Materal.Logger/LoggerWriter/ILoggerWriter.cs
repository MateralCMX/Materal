namespace Materal.Logger.LoggerWriter
{
    /// <summary>
    /// 日志写入器
    /// </summary>
    public interface ILoggerWriter
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        Task WriteLoggerAsync(LoggerWriterModel model);
        /// <summary>
        /// 关闭
        /// </summary>
        Task ShutdownAsync();
        /// <summary>
        /// 日志配置变更事件
        /// </summary>
        Action<LoggerConfig, IServiceProvider>? OnLoggerConfigChanged { get; }
    }
    /// <summary>
    /// 日志写入器
    /// </summary>
    public interface ILoggerWriter<TModel> : ILoggerWriter
        where TModel : LoggerWriterModel
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="model"></param>
        Task WriteLoggerAsync(TModel model);
    }
}
