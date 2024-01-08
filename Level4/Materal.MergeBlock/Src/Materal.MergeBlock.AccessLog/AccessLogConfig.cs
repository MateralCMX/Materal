namespace Materal.MergeBlock.AccessLog
{
    /// <summary>
    /// 访问日志配置
    /// </summary>
    public class AccessLogConfig
    {
        /// <summary>
        /// 配置键
        /// </summary>
        public static string ConfigKey { get; } = "AccessLog";
        /// <summary>
        /// 显示异常
        /// </summary>
        public int MaxBodySize { get; set; } = 2048;
    }
}
