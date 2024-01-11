namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// 批量目标配置
    /// </summary>
    public abstract class BatchLoggerTargetConfig : TargetConfig
    {
        /// <summary>
        /// 批量处理大小
        /// </summary>
        public BatchConfig Batch { get; set; } = new();
    }
    /// <summary>
    /// 批量目标配置
    /// </summary>
    public abstract class BatchLoggerTargetConfig<TLoggerWriter> : BatchLoggerTargetConfig
        where TLoggerWriter : ILoggerWriter
    {
        private TLoggerWriter? _loggerWriter;
        /// <summary>
        /// 获得日志写入器
        /// </summary>
        public override ILoggerWriter GetLoggerWriter() => _loggerWriter ??= typeof(TLoggerWriter).Instantiation<TLoggerWriter>(this);
    }
}
