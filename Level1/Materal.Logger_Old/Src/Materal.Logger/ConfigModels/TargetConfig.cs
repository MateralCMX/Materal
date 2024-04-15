namespace Materal.Logger.ConfigModels
{
    /// <summary>
    /// 目标配置
    /// </summary>
    public abstract class TargetConfig
    {
        /// <summary>
        /// 启用标识
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public abstract string Type { get; }
        /// <summary>
        /// 获得日志写入器
        /// </summary>
        /// <param name="serviceProvider"></param>
        public abstract ILoggerWriter GetLoggerWriter(IServiceProvider serviceProvider);
    }
    /// <summary>
    /// 目标配置
    /// </summary>
    public abstract class TargetConfig<TLoggerWriter> : TargetConfig
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
