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
        public override ILoggerWriter GetLoggerWriter() => _loggerWriter ??= typeof(TLoggerWriter).Instantiation<TLoggerWriter>(this);
    }
}
