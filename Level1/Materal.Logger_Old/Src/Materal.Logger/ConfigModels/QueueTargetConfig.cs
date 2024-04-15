namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// 队列目标配置
    /// </summary>
    public abstract class QueueTargetConfig : TargetConfig
    {
    }
    /// <summary>
    /// 队列目标配置
    /// </summary>
    public abstract class QueueTargetConfig<TLoggerWriter> : QueueTargetConfig
        where TLoggerWriter : ILoggerWriter
    {
        private TLoggerWriter? _loggerWriter;
        /// <summary>
        /// 获得日志写入器
        /// </summary>
        /// <param name="serviceProvider"></param>
        public override ILoggerWriter GetLoggerWriter(IServiceProvider serviceProvider) => _loggerWriter ??= typeof(TLoggerWriter).Instantiation<TLoggerWriter>(serviceProvider, [this]);
    }
}
