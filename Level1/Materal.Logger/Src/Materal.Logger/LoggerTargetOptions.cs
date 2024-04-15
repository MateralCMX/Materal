namespace Materal.Logger
{
    /// <summary>
    /// 日志目标配置
    /// </summary>
    public abstract class LoggerTargetOptions
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; } = true;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; } = string.Empty;
    }
}
